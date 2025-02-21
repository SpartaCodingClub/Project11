using DG.Tweening;
using UnityEngine;

public class Utility
{
    public static T FindComponent<T>(GameObject gameObject, string name) where T : Component
    {
        var components = gameObject.GetComponentsInChildren<T>(true);
        foreach (var component in components)
        {
            if (component.name.Equals(name))
            {
                return component;
            }
        }

        Debug.LogWarning($"Failed to FindComponent<{typeof(T).Name}>({gameObject.name}, {name})");
        return null;
    }

    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        if (gameObject.TryGetComponent<T>(out var component))
        {
            return component;
        }

        return gameObject.AddComponent<T>();
    }

    public static Sequence RecyclableSequence()
    {
        return DOTween.Sequence().Pause().SetAutoKill(false);
    }

    public static void SetPositionX(Transform transform, float x)
    {
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }

    public static void SetPositionY(Transform transform, float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
    }

    public static void SetPositionZ(Transform transform, float z)
    {
        Vector3 position = transform.position;
        position.z = z;
        transform.position = position;
    }
}