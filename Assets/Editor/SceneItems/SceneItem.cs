using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    [Serializable]
    public class SceneItem : Item
    {
        public string GameObjectPath;

        private const char PathSeparator = '/';

        private GameObject _cachedObject;

        public SceneData Data { get; set; }

        private SceneItem()
        {
        }
        
        private SceneItem(SceneData data)
        {
            Data = data;
        }

        public static SceneItem TryCreateFromObject(Object obj, SceneData data)
        {
            if (obj is GameObject gameObject)
            {
                SceneItem item = new SceneItem(data);

                return item.TrySetGameObject(gameObject) ? item : null;
            }

            return null;
        }

        public override void RenderItem(Rect rect)
        {
            GameObject obj = (GameObject) EditorGUI.ObjectField(rect, _cachedObject, typeof(GameObject), true);

            if (obj != _cachedObject && IsObjectValid(obj))
                TrySetGameObject(obj);
        }

        private bool TrySetGameObject(GameObject obj)
        {
            if (!IsObjectValid(obj) || !Data.IsAcceptableObject(obj))
                return false;

            GameObjectPath = GetPath(obj.transform);
            _cachedObject = obj;

            SetChanged();

            return true;
        }

        public static bool IsObjectValid(Object obj)
        {
            if (obj is GameObject gameObject)
            {
                return gameObject.scene.IsValid() && gameObject.scene.isLoaded;
            }

            return false;
        }

        private static string GetPath(Transform obj)
        {
            string path = $"{obj.name}";
            while (obj.parent != null)
            {
                obj = obj.parent;
                path = $"{obj.name}{PathSeparator}{path}";
            }

            return path;
        }

        public override bool TryCacheObject()
        {
            if (_cachedObject)
                return true;

            string[] path = GameObjectPath.Split(PathSeparator);

            Transform[] currentDepthObjects = Data.CachedScene.GetRootGameObjects().Select(obj => obj.transform).ToArray();

            Transform foundObj = null;

            foreach (Transform obj in currentDepthObjects)
            {
                foundObj = GameObjectSearch(obj, path, 0);

                if (foundObj)
                {
                    _cachedObject = foundObj.gameObject;

                    return true;
                }
            }

            return false;
        }

        private Transform GameObjectSearch(Transform parent, string[] path, int pathDepth)
        {
            if (!parent.name.Equals(path[pathDepth]))
                return null;

            if (pathDepth >= path.Length - 1)
            {
                return parent;
            }

            foreach (Transform child in parent.GetComponentsInChildren<Transform>())
            {
                Transform foundObj = GameObjectSearch(child, path, pathDepth + 1);

                if (foundObj)
                {
                    return foundObj;
                }
            }

            return null;
        }
    }
}