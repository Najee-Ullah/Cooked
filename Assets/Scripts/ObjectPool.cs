using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly Queue<T> objects = new Queue<T>();
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T newObj = GameObject.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(false);
            objects.Enqueue(newObj);
        }
    }

    public T Get()
    {
        if (objects.Count == 0)
        {
            AddObject();
        }

        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

    private void AddObject()
    {
        T newObj = GameObject.Instantiate(prefab, parent);
        newObj.gameObject.SetActive(false);
        objects.Enqueue(newObj);
    }
}
