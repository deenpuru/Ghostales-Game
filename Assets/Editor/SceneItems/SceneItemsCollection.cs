using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    [Serializable]
    public class SceneItemsCollection : ItemsCollection<SceneItem>
    {
        public SceneData Data;

        private SceneItemsCollection()
        {
        }

        public SceneItemsCollection(string sceneName)
        {
            Data = new SceneData(sceneName);
        }

        public static SceneItemsCollection TryCreateFromObject(Object obj)
        {
            if (obj is GameObject gameObject)
            {
                SceneItemsCollection item = new SceneItemsCollection(gameObject.scene.name);

                return item.TryAddItem(obj) ? item : null;
            }
            else
                return null;
        }

        public override bool TryAddItem(Object obj)
        {
            SceneItem item = SceneItem.TryCreateFromObject(obj, Data);

            if (item == null)
                return false;

            Add(item);
            return true;
        }

        public override void TryCacheObjects()
        {
            if (!TrySearchForScene())
                return;

            base.TryCacheObjects();
        }

        private bool TrySearchForScene()
        {
            if (Data.CachedScene.IsValid())
                return true;

            Data.CachedScene = SceneManager.GetSceneByName(Data.SceneName);

            return Data.CachedScene.IsValid();
        }

        public override bool ShouldBeRendered()
        {
            return Data.SceneName == null || SceneManager.GetSceneByName(Data.SceneName).isLoaded;
        }

        protected override void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, Data.SceneName, EditorStyles.boldLabel);
        }

        protected override void InitItemAfterDeserialize(SceneItem item)
        {
            item.Data = Data;

            base.InitItemAfterDeserialize(item);
        }
    }
}