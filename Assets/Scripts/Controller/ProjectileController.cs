using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    #region Inspector
    [Header("Required")]
    [SerializeField]
    private float speed = 5.0f;

    [Header("Optional")]
    [SerializeField]
    private ObjectController effect;
    #endregion

    private Rigidbody2D _rigidbody;

    private float damage;

    private readonly List<int> targetLayers = new();

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetProjectile(bool isPlayer, float damage, Vector2 targetDirection)
    {
        this.damage = damage;

        if (isPlayer)
        {
            targetLayers.Add(LayerMask.NameToLayer(Define.Monster));
            targetLayers.Add(LayerMask.NameToLayer(Define.Boss));
        }
        else
        {
            targetLayers.Add(LayerMask.NameToLayer(Define.Player));
        }
        targetLayers.Add(LayerMask.NameToLayer(Define.Obstacle));

        if (speed == 0.0f)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                if (IsDead)
                {
                    return;
                }

                Destroy();
            });
        }

        var z = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, z);
        _rigidbody.velocity = targetDirection * speed;

        Managers.Audio.Play(Clip.SoundFX_Shooting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayers.Count == 0)
        {
            return;
        }

        GameObject targetObject = collision.gameObject;
        foreach (var targetLayer in targetLayers)
        {
            if (targetObject.layer == targetLayer)
            {
                Destroy();
                break;
            }
        }
    }

    public override void Destroy()
    {
        if (effect != null)
        {
            string key = effect.name;
            Vector2 position = speed == 0.0f ? Managers.Game.Player.transform.position : transform.position;
            GameObject effectObject = Managers.Resource.Instantiate(key, null, position, Define.EFFECT);
            var @object = effectObject.GetComponent<ObjectController>();
            @object.Death();
        }

        base.Destroy();
        targetLayers.Clear();
    }
}