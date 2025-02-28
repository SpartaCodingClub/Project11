using UnityEngine;

public class ResourceManager
{
    public T Instantiate<T>(Transform parent, Vector2 position, string folderName = Define.OBJECT) where T : BaseController
    {
        string key = typeof(T).Name;
        GameObject gameObject = Instantiate(key, parent, position, folderName);
        if (gameObject.TryGetComponent<T>(out var @base) == false)
        {
            Debug.LogWarning($"Failed to GetComponent<{typeof(T).Name}>()");
            return null;
        }

        return @base;
    }

    public GameObject Instantiate(string key, Transform parent, Vector2 position, string folderName = Define.OBJECT)
    {
        GameObject gameObject = Managers.Pool.TryPop(key);
        if (gameObject == null)
        {
            string path = $"Prefabs/{folderName}/{key}";
            GameObject original = Resources.Load<GameObject>(path);
            if (original == null)
            {
                Debug.LogWarning($"Failed to Load<GameObject>({path})");
                return null;
            }

            gameObject = Instantiate(original, parent);
        }
        else
        {
            gameObject.transform.SetParent(parent);
        }

        if (gameObject.TryGetComponent<BaseController>(out var @base))
        {
            @base.Birth();
        }

        gameObject.transform.position = position;
        return gameObject;
    }

    public GameObject Instantiate(GameObject original, Transform parent)
    {
        GameObject gameObject = Object.Instantiate(original, parent);
        gameObject.name = original.name;

        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Poolable>(out var poolable))
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(gameObject);
    }
}