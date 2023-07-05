using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class AssetGroup<TAsset> where TAsset : Object
    {
        public TAsset[] Assets { get; private set; }

        private Dictionary<string, TAsset> assetsNamedDict = new Dictionary<string, TAsset>();
        private Dictionary<int, TAsset> assetsIdDict = new Dictionary<int, TAsset>();

        public void Load(string path, bool showLog = true)
        {
            Assets = Resources.LoadAll<TAsset>(path);

            if (showLog)
            {
                Debug.Log($"Path : <{path}> \nLoaded count : <{Assets.Length}>");
            }

            if (Assets.Length == 0)
                return;

            assetsNamedDict.Clear();
            assetsIdDict.Clear();

            for(int i = 0; i < Assets.Length; i++)
            {
                TAsset asset = Assets[i];

                assetsNamedDict.Add(asset.name, asset);
                assetsIdDict.Add(asset.GetInstanceID(), asset);
            }
        }

        public TAsset Get(string name)
        {
            if (assetsNamedDict.TryGetValue(name, out TAsset asset))
                return asset;

            Debug.LogError($"Asset name <{name}> not found or exited");
            return null;
        }

        public TAsset Get(int id)
        {
            if (assetsIdDict.TryGetValue(id, out TAsset asset))
                return asset;

            Debug.LogError($"Asset id <{id}> not found or exited");
            return null;
        }

        public TAsset GetAt(int index)
        {
            return Assets[index];
        }
    }
}