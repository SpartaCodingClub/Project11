using DG.Tweening;
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

    protected Rigidbody2D _rigidbody;
    private int obstacleLayer;
    private float damage;

    private int targetLayer;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.NameToLayer(Define.Obstacle);
    }

    public void SetProjectile(bool isPlayer, float damage, Vector2 targetDirection)
    {
        this.damage = damage;

        if (isPlayer)
        {
            targetLayer = LayerMask.GetMask(Define.Monster, Define.Boss, Define.Obstacle);
        }
        else
        {
            targetLayer = LayerMask.GetMask(Define.Player, Define.Obstacle);
        }

        if (speed == 0.0f)
        {
            DOVirtual.DelayedCall(0.2f, () =>
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead)
        {
            return;
        }

        GameObject targetObject = collision.gameObject;
        int targetObjectLayer = 1 << targetObject.layer;
        if ((targetObjectLayer & targetLayer) != 0)
        {
            if (targetObject.layer == obstacleLayer)
            {
                Destroy();
                return;
            }

            var @object = targetObject.GetComponent<ObjectController>();
            if (@object.IsDead)
            {
                return;
            }

            @object.StatHandler.OnDamage(damage);
            Destroy();
        }
    }

    public override void Destroy()
    {
        if (effect != null)
        {
            string key = effect.name;
            Vector2 position = speed == 0.0f ? Managers.Game.Player.transform.position : transform.position;
            GameObject effectObject = Managers.Resource.Instantiate(key, null, position, Define.EFFECT);
            effectObject.GetComponent<ObjectController>().Death();
        }

        if (IsDead)
        {
            return;
        }

        base.Destroy();
    }
}