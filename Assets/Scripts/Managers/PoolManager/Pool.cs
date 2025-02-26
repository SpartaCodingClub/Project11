using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    public Pool(Poolable poolable, Transform parent)
    {
        transform = new GameObject(poolable.name).transform;
        transform.SetParent(parent);

        original = poolable.gameObject;
        objectPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private readonly Transform transform;
    private readonly GameObject original;
    private readonly IObjectPool<GameObject> objectPool;

    public void Push(Poolable poolable)
    {
        objectPool.Release(poolable.gameObject);
    }

    public GameObject Pop()
    {
        return objectPool.Get();
    }

    private GameObject CreateFunc()
    {
        GameObject gameObject = Object.Instantiate(original);
        gameObject.name = original.name;

        return gameObject;
    }

    private void ActionOnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void ActionOnRelease(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        gameObject.transform.SetParent(transform);
        gameObject.SetActive(false);
    }

    private void ActionOnDestroy(GameObject gameObject)
    {
        Object.Destroy(gameObject);
    }
}