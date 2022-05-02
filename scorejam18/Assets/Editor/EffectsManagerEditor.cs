using UnityEngine;
using UnityEditor;
using Gisha.Effects.VFX;

[CustomEditor(typeof(VFXManager))]
public class EffectsManagerEditor : Editor
{
    private VFXManager manager;

    private SerializedProperty effectsProp;

    #region Styles
    GUIStyle bold;
    GUIStyle collectionBlock;
    GUIStyle buttonsBlock;
    #endregion

    private void OnEnable()
    {
        manager = target as VFXManager;

        effectsProp = serializedObject.FindProperty("effectsCollection");
    }

    public override void OnInspectorGUI()
    {
        #region Styles
        bold = new GUIStyle();
        bold.fontSize = 12;
        bold.fontStyle = FontStyle.Bold;
        bold.normal.textColor = Color.white;

        collectionBlock = new GUIStyle("box");
        collectionBlock.margin = new RectOffset(0, 0, 0, 15);
        collectionBlock.padding = new RectOffset(0, 0, 10, 10);

        buttonsBlock = new GUIStyle();
        buttonsBlock.margin = new RectOffset(0, 0, 10, 0);
        #endregion

        EditorGUILayout.LabelField("Effects", bold);

        EditorGUILayout.BeginVertical(collectionBlock);
        InitCollection(effectsProp, "Effects");
        EditorGUILayout.EndVertical();
    }


    private void InitCollection(SerializedProperty collection, string shortName)
    {
        Color defColor = GUI.backgroundColor;

        EditorGUILayout.LabelField(shortName.ToUpper(), bold);
        serializedObject.Update();
        EditorGUILayout.PropertyField(collection, true);

        // Importer Call subblock.
        EditorGUILayout.BeginHorizontal(buttonsBlock);

        defColor = GUI.color;
        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button(string.Format("Call {0} Importer", shortName)))
        {
            Importer.ShowWindow();
            Importer.InitData(manager, shortName.ToLower() + "Collection");
        }

        GUI.backgroundColor = defColor;

        // Clear subblock.
        defColor = GUI.color;
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button(string.Format("Clear {0} Collection", shortName)))
            collection.ClearArray();
        GUI.backgroundColor = defColor;

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

}
