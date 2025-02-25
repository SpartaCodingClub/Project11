using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraManager
{
    public readonly Camera Main = new GameObject(nameof(CameraManager)).AddComponent<Camera>();

    public Transform Target { get; set; }

    private Transform transform;

    public void Initialize()
    {
        transform = Main.transform;
        transform.SetParent(Managers.Instance.transform);
        transform.position = 10.0f * Vector3.back;

        Main.orthographic = true;
        Main.orthographicSize = 10;

        Light2D light = Main.gameObject.AddComponent<Light2D>();
        light.lightType = Light2D.LightType.Global;
        light.intensity = 0.1f;
    }

    public void FixedUpdate()
    {
        if (Target == null)
        {
            return;
        }

        float x = Mathf.Lerp(transform.position.x, Target.position.x, 0.02f);
        float y = Mathf.Lerp(transform.position.y, Target.position.y, 0.02f);
        transform.position = new(x, y, transform.position.z);
    }
}