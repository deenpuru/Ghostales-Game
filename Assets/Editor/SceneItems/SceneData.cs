using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace FavouriteList
{
    [Serializable]
    public class SceneData
    {
        public string SceneName;
        [NonSerialized] public Scene CachedScene;

        public SceneData(string sceneName)
        {
            SceneName = sceneName;
        }
        
        public bool IsAcceptableObject(Object obj)
        {
            if (obj is GameObject gameObject)
            {
                string objSceneName = gameObject.scene.name;

                return !string.IsNullOrWhiteSpace(objSceneName) && objSceneName.Equals(SceneName);
            }

            return false;
        }
    }
}