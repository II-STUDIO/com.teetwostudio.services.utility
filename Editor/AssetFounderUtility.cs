#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Services.Utility
{
    public static class AssetFounderUtility
    {
        public static void FoundExistOrCreateOneSO<T>(string directory, string assetName, out T output) where T : ScriptableObject
        {
            string typeName = typeof(T).Name;

            var guids = AssetDatabase.FindAssets($"t:{typeName}");

            output = null;

            if (guids.Length > 0)
                output = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(T)) as T;

            if (output != null)
            {
                return;
            }
            else
            {
                output = ScriptableObject.CreateInstance<T>();

                string path = directory + assetName;

                string targetDirectory = $"{Application.dataPath}/{directory}/";
                string resultPath = $"Assets/{path}";

                if (!Directory.Exists(targetDirectory))
                    Directory.CreateDirectory(targetDirectory);

                AssetDatabase.Refresh();

                AssetDatabase.CreateAsset(output, resultPath);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif