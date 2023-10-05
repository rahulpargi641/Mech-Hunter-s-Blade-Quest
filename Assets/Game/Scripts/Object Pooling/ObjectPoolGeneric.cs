using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGeneric<T> : MonoBehaviour
{
    private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

    protected virtual T GetItemFromPool()
    {
        if (pooledItems.Count > 0)
        {
            PooledItem<T> pooledItem = pooledItems.Find(item => item.IsUsed == false);
            if (pooledItem != null)
            {
                pooledItem.IsUsed = true;
                return pooledItem.Item;
            }
        }

        return CreateNewItemAndStoreInPool();
    }


    private T CreateNewItemAndStoreInPool()
    {
        PooledItem<T> item = new PooledItem<T>();
        item.Item = CreateItem();
        item.IsUsed = true;

        pooledItems.Add(item);

        return item.Item;
    }

    public virtual void ReturnItem(T gameObjectToReturn)
    {
        PooledItem<T> item = pooledItems.Find(item => item.Item.Equals(gameObjectToReturn));
        item.IsUsed = false;
        Debug.Log("Item returned to the pool" + item.Item);
    }
    protected virtual T CreateItem()
    {
        return default(T);
    }

    public virtual void Initialize(T item)
    {
        // item will be used to assign the member variable in child classes
    }

    private class PooledItem<T>
    {
        public T Item;
        public bool IsUsed;
    }
}
