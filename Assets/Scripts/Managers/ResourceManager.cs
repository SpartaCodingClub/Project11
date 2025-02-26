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

        if (@base is UI_Base)
        {
            @base.Birth();
        }
        else
        {
            @base.Stand();
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

            gameObject = Object.Instantiate(original, parent);
            gameObject.name = original.name;
        }
        else
        {
            gameObject.transform.SetParent(parent);
        }

        gameObject.transform.position = position;
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