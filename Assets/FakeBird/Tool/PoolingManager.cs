using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class PoolingManager
{
    private static Dictionary<int, Pool> _pools;

    private static void Init(GameObject prefab)
    {
        if (_pools == null)
        {
            _pools = new Dictionary<int, Pool>();
        }

        if (!_pools.ContainsKey(prefab.GetInstanceID()))
        {
            _pools[prefab.GetInstanceID()] = new Pool(prefab);
        }
    }

    public static GameObject Spawn(GameObject prefab)
    {
        Init(prefab);
        return _pools[prefab.GetInstanceID()].Spawn(Vector3.zero, quaternion.identity);
    }
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        Init(prefab);
        return _pools[prefab.GetInstanceID()].Spawn(position, quaternion, parent);
    }
    public static T Spawn<T>(T prefab) where T:Component
    {
        Init(prefab.gameObject);
        return _pools[prefab.gameObject.GetInstanceID()].Spawn<T>(Vector3.zero, quaternion.identity);
    }
    public static T Spawn<T>(T prefab, Vector3 position, Quaternion quaternion, Transform parent = null) where T:Component
    {
        Init(prefab.gameObject);
        return _pools[prefab.gameObject.GetInstanceID()].Spawn<T>(position, quaternion, parent);
    }

    public static void Despawn(GameObject prefab)
    {
        if (!prefab.activeSelf)
        {
            return;
        }
        Pool p = null;
        foreach (var pool in _pools.Values)
        {
            if (pool.IDObjects.Contains(prefab.GetInstanceID()))
            {
                p = pool;
                break;
            }
        }
        if (p != null)
        {
            p.Despawn(prefab);
        }
        else
        {
            Object.Destroy(prefab);
        }
    }
}

public class Pool
{
    private readonly Stack<GameObject> listGameObject;
    public readonly HashSet<int> IDObjects;
    private GameObject prefab;

    public Pool(GameObject gameObject)
    {
        prefab = gameObject;
        listGameObject = new Stack<GameObject>();
        IDObjects = new HashSet<int>();
    }

    public GameObject Spawn(Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        GameObject newObject;
        while (true)
        {
            if (listGameObject.Count <= 0)
            {
                newObject = Object.Instantiate(prefab, position, quaternion, parent);
                newObject.name = prefab.name;
                IDObjects.Add(newObject.GetInstanceID());
                return newObject;
            }
            newObject = listGameObject.Pop();
            newObject.transform.SetPositionAndRotation(position, quaternion);
            newObject.transform.parent = parent;
            newObject.SetActive(true);
            return newObject;
        }
    }

    public T Spawn<T>(Vector3 position, Quaternion quaternion, Transform parent = null) where T : Component
    {
        return Spawn(position, quaternion, parent).GetComponent<T>();
    }

    public void Despawn(GameObject gameObject)
    {
        gameObject.SetActive(false);
        listGameObject.Push(gameObject);
    }
}
