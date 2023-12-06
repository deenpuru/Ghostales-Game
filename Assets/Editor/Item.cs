using System;
using UnityEditor;
using UnityEngine;

namespace FavouriteList
{
    [Serializable]
    public abstract class Item
    {
        public event Action<Item> OnChanged;
        public event Action<Item> OnDeleted; 

        public abstract void RenderItem(Rect rect);
        public abstract bool TryCacheObject();

        public Item()
        {
        }

        public virtual void Render(Rect rect)
        {
            float buttonSize = rect.height;

            EditorGUILayout.BeginHorizontal();
            {
                Rect objRect = new Rect(rect.x, rect.y, rect.width - buttonSize, rect.height);
                
                RenderItem(objRect);

                Rect buttonRect = new Rect(rect.x + rect.width - buttonSize, rect.y, buttonSize, buttonSize);

                if (GUI.Button(buttonRect, "X"))
                    OnDeleted?.Invoke(this);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected void SetChanged()
        {
            OnChanged?.Invoke(this);
        }
    }
}