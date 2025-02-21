using UnityEngine;

public static class Extention
{
    public static T FindComponent<T>(this GameObject gameObject, string name) where T : Component
    {
        return Utility.FindComponent<T>(gameObject, name);
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        return Utility.GetOrAddComponent<T>(gameObject);
    }

    public static T GetOrAddComponent<T>(this Transform transform) where T : Component
    {
        return Utility.GetOrAddComponent<T>(transform.gameObject);
    }

    public static void SetPositionX(this Transform transform, float z)
    {
        Utility.SetPositionX(transform, z);
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        Utility.SetPositionY(transform, y);
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        Utility.SetPositionZ(transform, z);
    }
}