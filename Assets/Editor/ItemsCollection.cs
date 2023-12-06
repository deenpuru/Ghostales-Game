using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    [Serializable]
    public abstract class ItemsCollection<T> : IItemsCollection where T : Item
    {
        [SerializeReference] public List<T> Items;

        public float Height => _listView.GetHeight();
        public int ItemsCount => Items.Count;

        public event Action<IItemsCollection> OnChanged;

        protected ReorderableList _listView;
 
        public abstract bool TryAddItem(Object obj);
        public abstract bool ShouldBeRendered();
        protected abstract void DrawHeader(Rect rect);

        public ItemsCollection()
        {
            Items = new List<T>();

            InitListView();
        }

        private void InitListView()
        {
            _listView = new ReorderableList(Items, typeof(Item), true, true, false, false);
            _listView.drawElementCallback += DrawListElement;
            _listView.drawHeaderCallback += DrawHeader;
            _listView.onReorderCallback += OnReorder;
            _listView.footerHeight = 0;
        }

        ~ItemsCollection()
        {
            _listView.drawElementCallback -= DrawListElement;
            _listView.drawHeaderCallback -= DrawHeader;
            _listView.onReorderCallback -= OnReorder;
        }

        protected void Add(T newItem)
        {
            Items.Add(newItem);

            ItemsChanged(newItem);

            newItem.OnChanged += ItemsChanged;
            newItem.OnDeleted += Remove;
        }

        protected void Remove(Item item)
        {
            if (item is T concreteNewItem)
            {
                Items.Remove(concreteNewItem);

                ItemsChanged(item);
            }
        }

        public virtual void TryCacheObjects()
        {
            foreach (T item in Items)
            {
                item.TryCacheObject();
            }
        }

        protected void ItemsChanged(Item item)
        {
            OnChanged?.Invoke(this);
        }

        public virtual void Render(Rect rect)
        {
            _listView.DoList(rect);
        }

        protected virtual void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index > Items.Count - 1)
                return;

            Item item = Items[index];
            item.Render(rect);
        }

        private void OnReorder(ReorderableList list)
        {
            ItemsChanged(null);
        }

        public void InitAfterDeserialize()
        {
            foreach (T item in Items)
            { 
                InitItemAfterDeserialize(item);
            }

            InitListView();
        }

        protected virtual void InitItemAfterDeserialize(T item)
        {
            item.OnChanged += ItemsChanged;
            item.OnDeleted += Remove;
        }
    }
}