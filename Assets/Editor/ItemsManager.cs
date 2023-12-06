using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    [Serializable]
    public class ItemsManager
    {
        [SerializeReference] public List<IItemsCollection> Items = new List<IItemsCollection>();

        public Action OnChanged;

        public ItemsManager()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            EditorSceneManager.sceneOpened += OnSceneOpened;
        }

        ~ItemsManager()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            EditorSceneManager.sceneOpened -= OnSceneOpened;
        }

        public void Add(Object obj)
        {
            foreach (IItemsCollection item in Items)
            {
                if (item.TryAddItem(obj))
                {
                    ItemsChanged(null);
                    
                    return;
                }
            }

            AddItem(obj);

            ItemsChanged(null);
        }

        private void AddItem(Object obj)
        {
            SceneItemsCollection sceneItem = SceneItemsCollection.TryCreateFromObject(obj);

            if (sceneItem != null)
            {
                Add(sceneItem);
                return;
            }

            AssetItemsCollection assetItem = AssetItemsCollection.TryCreateFromObject(obj);

            if (assetItem != null)
                Add(assetItem);
        }

        public void Add(IItemsCollection itemsCollection)
        {
            itemsCollection.OnChanged += ItemsChanged;

            Items.Add(itemsCollection);
        }

        public void Clear()
        {
            Items.Clear();
            ItemsChanged(null);
        }

        public void MoveAfterItem(IItemsCollection source, IItemsCollection destination)
        {
            int sourceIndex = Items.IndexOf(source);
            int destinationIndex = Items.IndexOf(destination);

            Items.RemoveAt(sourceIndex);
            Items.Insert(destinationIndex, source);

            ItemsChanged(null);
        }

        public void ItemsChanged(IItemsCollection itemsCollection)
        {
            if (itemsCollection != null)
            {
                if (itemsCollection.ItemsCount == 0)
                {
                    Items.Remove(itemsCollection);
                }
            }

            OnChanged?.Invoke();
        }

        public void TryCacheAllObjects()
        {
            foreach (IItemsCollection sceneItem in Items)
            {
                sceneItem.TryCacheObjects();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            TryCacheAllObjects();
        }

        private void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            TryCacheAllObjects();
        }

        public void InitAfterDeserialize()
        {
            foreach (IItemsCollection sceneItems in Items)
            {
                sceneItems.InitAfterDeserialize();

                sceneItems.OnChanged += ItemsChanged;
            }
        }
    }
}