using System;
using UnityEditor;
using UnityEngine;

namespace FavouriteList
{
    public class EditorPrefsSaveManager
    {
        private const string ToolName = "FavouriteList";
        private const string FeatureName = "Scene";

        public void Save(ItemsManager itemsManager)
        {
            string json = JsonUtility.ToJson(itemsManager);

            EditorPrefs.SetString(GetSaveKey(), json);
        }

        public ItemsManager Load()
        {
            try
            {
                string json = EditorPrefs.GetString(GetSaveKey());

                ItemsManager items = JsonUtility.FromJson<ItemsManager>(json);
                items.InitAfterDeserialize();

                return items;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetSaveKey()
        {
            return $"{ToolName}.{Application.unityVersion}.{FeatureName}.{GetProjectName()}";
        }

        private string GetProjectName()
        {
            return $"{Application.companyName}.{Application.productName}";
        }
    }
}