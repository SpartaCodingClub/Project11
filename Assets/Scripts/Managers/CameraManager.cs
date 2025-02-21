using UnityEngine;

public class CameraManager
{
    public readonly Camera Main = new GameObject(nameof(CameraManager)).AddComponent<Camera>();

    public void Initialize()
    {
        Main.transform.SetParent(Managers.Instance.transform);
        Main.transform.position = Vector3.back;
        Main.orthographic = true;
    }
}