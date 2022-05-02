using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace AlmostEngine.Screenshot
{
    public static class InputSystemSupport
    {
        public static void ToggleInputSystemSupport(bool enable = true)
        {
            // Define symbol
            RemovePermissionNeeds.ToggleDefineSymbol("USC_INPUT_SYSTEM", enable);
            // Assembly refs
            var almostEngineAssemblies = FindAsmdefFiles("AlmostEngine");
            var inputSystemAssemblies = FindAsmdefFiles("InputSystem");
            var inputSystemAssemblyName = inputSystemAssemblies.Count > 0 ? inputSystemAssemblies[0] : "";
            foreach (var almostAssemblyPath in almostEngineAssemblies)
            {
                ToggleAssemblyReference(almostAssemblyPath, inputSystemAssemblyName, enable);
            }
        }

        public static void ToggleAssemblyReference(string assemblyFilePath, string assemblyRefName, bool enable)
        {
            var file = System.IO.File.ReadAllText(assemblyFilePath);
            var assemblyName = System.IO.Path.GetFileNameWithoutExtension(assemblyFilePath);
            var refName = System.IO.Path.GetFileNameWithoutExtension(assemblyRefName);
            if (enable && !file.Contains(refName))
            {
                file = file.Replace("\"references\": [", "\"references\": [" + "\"" + refName + "\",");
                file = file.Replace(",]", "]");
                Debug.Log("Adding assembly reference " + refName + " to " + assemblyName + "\n" + file);
                System.IO.File.WriteAllText(assemblyFilePath, file);
                AssetDatabase.Refresh();
            }
            else if (!enable && file.Contains(refName))
            {
                file = file.Replace("\"" + refName + "\",", "");
                file = file.Replace("\"" + refName + "\"", "");
                Debug.Log("Removing assembly reference " + refName + " from " + assemblyName + "\n" + file);
                System.IO.File.WriteAllText(assemblyFilePath, file);
                AssetDatabase.Refresh();
            }
        }

        public static List<string> FindAsmdefFiles(string name)
        {
            var guids = AssetDatabase.FindAssets("t:asmdef").ToList<string>();
            var files = new List<string>();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.Contains(name))
                {
                    files.Add(path);
                }
            }
            return files;
        }
    }
}