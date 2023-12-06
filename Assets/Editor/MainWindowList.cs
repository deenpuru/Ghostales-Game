using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FavouriteList
{
    public class MainWindowList
    {
        private ItemsManager _itemsManager;
        private ReorderableList _listView;
        private Vector2 _scrollPosition;

        private List<IItemsCollection> _itemsManagerList = new List<IItemsCollection>();

        public MainWindowList(ItemsManager itemsManager)
        {
            _itemsManager = itemsManager;

            _listView = new ReorderableList(_itemsManagerList, typeof(IItemsCollection), true, false, false, false);
            
            _listView.drawElementCallback += RenderListItem;
            _listView.elementHeightCallback += ListElementHeight;
            _listView.onReorderCallbackWithDetails += OnReorder;

            _listView.headerHeight = 0;
            _listView.footerHeight = 0;

            PopulateCurrentSceneItems();

            _itemsManager.OnChanged += OnItemsChanged;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            EditorSceneManager.sceneOpened += OnSceneOpened;
            EditorSceneManager.sceneClosed += OnSceneClosed;
        }

        ~MainWindowList()
        {
            _listView.drawElementCallback -= RenderListItem;
            _listView.elementHeightCallback -= ListElementHeight;
            _listView.onReorderCallbackWithDetails -= OnReorder;

            _itemsManager.OnChanged -= OnItemsChanged;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            EditorSceneManager.sceneClosed -= OnSceneClosed;
        }

        public void Render()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            {
                if (_itemsManagerList.Count != 0)
                    _listView.DoLayoutList();
            }
            GUILayout.EndScrollView();
        }

        public void PopulateCurrentSceneItems()
        {
            _itemsManagerList.Clear();

            _itemsManagerList.AddRange(_itemsManager.Items.Where(item => item.ShouldBeRendered()));
        }

        private void RenderListItem(Rect rect, int index, bool isActive, bool isFocused)
        {
            GUI.changed = true;

            if (index >= _itemsManagerList.Count)
                return;

            IItemsCollection item = _itemsManagerList[index];

            item.Render(rect);
        }

        private float ListElementHeight(int index)
        {
            IItemsCollection item = _itemsManagerList[index];

            return item.Height;
        }

        private void OnReorder(ReorderableList list, int oldIndex, int newIndex)
        {
            IItemsCollection source = _itemsManagerList[oldIndex];
            IItemsCollection destination = _itemsManagerList[newIndex];

            _itemsManager.MoveAfterItem(destination, source);

            PopulateCurrentSceneItems();
        }

        private void OnItemsChanged()
        {
            PopulateCurrentSceneItems();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            PopulateCurrentSceneItems();
        }

        private void OnSceneOpened(Scene scene, OpenSceneMode loadMode)
        {
            PopulateCurrentSceneItems();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            PopulateCurrentSceneItems();
        }

        private void OnSceneClosed(Scene scene)
        {
            PopulateCurrentSceneItems();
        }
    }
}