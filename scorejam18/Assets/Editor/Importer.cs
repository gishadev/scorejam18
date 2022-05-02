using Gisha.Effects;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Importer : EditorWindow
{
    #region Singleton
    public static Importer Instance { private set; get; }
    #endregion

    private string folderPath;

    private List<ResourceData> resources = new List<ResourceData>();
    private List<ResourceData> output = new List<ResourceData>();

    [SerializeField] private ImportTarget importTarget;
    [SerializeField] private string importCollection;

    private Color selectedColor = Color.cyan;
    private Color unselectedColor = Color.black;
    private string searchField;

    private bool IsLoadedResources { get => resources != null && resources.Count > 0; }

    Vector2 scrollPos = Vector2.zero;

    #region Styles
    GUIStyle descText;
    #endregion

    [MenuItem("Tools/Importer")]
    public static void ShowWindow()
    {
        Importer window = (Importer)EditorWindow.GetWindow(typeof(Importer));

        window.Show();
    }

    public static void InitData(ImportTarget _importTarget, string _importCollection)
    {
        Instance.importTarget = _importTarget;
        Instance.importCollection = _importCollection;
    }

    private List<ResourceData> GetResourcesAtPath(string _path)
    {
        if (!Directory.Exists(_path))
        {
            Debug.LogError("Folder wasn't found.");
            return null;
        }

        string[] pathes = Directory.GetFiles(_path);
        List<ResourceData> result = new List<ResourceData>();

        for (int i = 0; i < pathes.Length; i++)
        {
            Object asset = AssetDatabase.LoadAssetAtPath(pathes[i], typeof(Object));
            if (asset == null) continue;

            ResourceData r = new ResourceData(asset.name, asset);
            result.Add(r);
        }

        return result;
    }

    private void ShowElement(ResourceData data, int index)
    {
        Color defColor = GUI.backgroundColor;
        GUI.backgroundColor = data.isSelected ? selectedColor : unselectedColor;

        EditorGUILayout.BeginHorizontal("box");

        data.isSelected = EditorGUILayout.Toggle(data.isSelected, GUILayout.Width(35f));
        EditorGUILayout.LabelField((index + 1).ToString(), GUILayout.Width(75f));
        EditorGUILayout.LabelField(data.name);
        EditorGUILayout.ObjectField(data.o, typeof(Object), false);

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = defColor;
    }

    void OnEnable()
    {
        Instance = this;

        #region Styles
        descText = new GUIStyle();
        descText.fontSize = 14;
        descText.fontStyle = FontStyle.Bold;
        descText.normal.textColor = Color.white;
        #endregion
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Get Resources:", descText);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        // Selection block.
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Path:", folderPath);
        if (GUILayout.Button("Browse", GUILayout.MaxWidth(100f)))
            folderPath = "Assets/" + EditorUtility.OpenFolderPanel("Local Resources Directory", "", "").Substring(Application.dataPath.Length);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Get Resources")) resources = GetResourcesAtPath(folderPath);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select All")) resources.ForEach(x => x.isSelected = true);
        if (GUILayout.Button("Deselect All")) resources.ForEach(x => x.isSelected = false);
        EditorGUILayout.EndHorizontal();

        searchField = EditorGUILayout.TextField("Search:", searchField);

        // Resources block.
        if (IsLoadedResources)
        {
            EditorGUILayout.LabelField("Select Resources:", descText);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false);
            output = new List<ResourceData>();
            for (int i = 0; i < resources.Count; i++)
            {
                if (string.IsNullOrEmpty(searchField) || resources[i].name.Contains(searchField))
                {
                    ShowElement(resources[i], i);

                    if (resources[i].isSelected)
                        output.Add(resources[i]);
                }
            }

            EditorGUILayout.EndScrollView();

            // Import block.
            EditorGUILayout.LabelField("Import:", descText);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            importTarget = EditorGUILayout.ObjectField("Import Target:", importTarget, typeof(ImportTarget), true) as ImportTarget;
            importCollection = EditorGUILayout.TextField("Collection Name:", importCollection, GUILayout.MinWidth(100f));
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Import"))
                if (importTarget != null)
                {
                    importTarget.Import(importCollection, output.ToArray());
                    Debug.Log("Importing Resources...");
                }
                    
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }
}