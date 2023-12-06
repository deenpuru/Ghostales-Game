using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FavouriteList
{
    public interface IItemsCollection
    {
        bool TryAddItem(Object obj);
        void Render(Rect rect);
        void TryCacheObjects();
        bool ShouldBeRendered();
        void InitAfterDeserialize();
        float Height { get; }
        int ItemsCount { get; }

        event Action<IItemsCollection> OnChanged;
    }
}