using UnityEngine;

public class ProjectileController : BaseController
{
    #region Inspector
    [SerializeField]
    private float speed = 5.0f;
    #endregion

    private Rigidbody2D _rigidbody;

    private float damage;
    private int obstacleLayer;
    private int targetLayer;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetProjectile(bool isPlayer, float damage, Vector2 targetDirection)
    {
        this.damage = damage;
        obstacleLayer = LayerMask.NameToLayer(Define.Obstacle);

        if (isPlayer)
        {
            targetLayer = LayerMask.NameToLayer(Define.Monster);
        }
        else
        {
            targetLayer = LayerMask.NameToLayer(Define.Player);
        }

        var z = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, z);
        _rigidbody.velocity = targetDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject targetObject = collision.gameObject;
        if (targetObject.layer == obstacleLayer)
        {
            Destroy();
            return;
        }

        if (targetObject.layer != targetLayer)
        {
            return;
        }

        Destroy();
    }
}