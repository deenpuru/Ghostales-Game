using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    [Serializable]
    public class AssetItem : Item
    {
        public string Guid;
        private Object _cachedObject;

        private AssetItem()
        {
        }

        public AssetItem(string guid)
        {
            Guid = guid;
        }

        public static AssetItem TryCreateFromObject(Object obj)
        {
            AssetItem item = new AssetItem();

            return item.TrySetObject(obj) ? item : null;
        }

        public bool TrySetObject(Object obj)
        {
            if (!IsObjectValid(obj))
                return false;

            Guid = GetGuid(obj);
            _cachedObject = obj;

            SetChanged();

            return true;
        }

        private static string GetGuid(Object obj)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            string guid = AssetDatabase.AssetPathToGUID(path);

            return guid;
        }

        private static bool IsObjectValid(Object obj)
        {
            return !string.IsNullOrWhiteSpace(GetGuid(obj));
        }

        public override void RenderItem(Rect rect)
        {
            Object obj = EditorGUI.ObjectField(rect, _cachedObject, typeof(Object), false);

            if (obj != _cachedObject && IsObjectValid(obj))
                TrySetObject(obj);
        }

        public override bool TryCacheObject()
        {
            if (_cachedObject)
                return true;

            string path = AssetDatabase.GUIDToAssetPath(Guid);
            _cachedObject = AssetDatabase.LoadAssetAtPath<Object>(path);

            return _cachedObject;
        }
    }
}