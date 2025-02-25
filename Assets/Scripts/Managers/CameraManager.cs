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

        Transform child = new GameObject(nameof(Light2D)).transform;
        child.SetParent(transform);

        Light2D light = child.gameObject.AddComponent<Light2D>();
        light.lightType = Light2D.LightType.Global;
        light.intensity = 0.1f;

        Rigidbody2D rigidbody = transform.gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0.0f;
        rigidbody.freezeRotation = true;
        rigidbody.excludeLayers = ~LayerMask.GetMask(LayerMask.LayerToName(1));

        transform.gameObject.AddComponent<BoxCollider2D>().size = new(12.0f, 20.0f);
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