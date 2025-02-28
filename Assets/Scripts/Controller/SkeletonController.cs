using UnityEngine;

public class SkeletonController : ObjectController
{
    private bool onTriggerStay;
    private ProjectileHandler projectileHandler;
    private StatHandler statHandler;
    [SerializeField] private ProjectileController[] prefabs;

    protected override void Initialize()
    {
        base.Initialize();

        CircleCollider2D collider = GetComponentInChildren<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = StatHandler.AttackRange;

        projectileHandler = gameObject.GetComponent<ProjectileHandler>();
        statHandler = gameObject.GetComponent<StatHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        projectileHandler.SpreadAngle = 0.5f;
        projectileHandler.ProjectileCount = 60;
        onTriggerStay = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
            return;

        Attack(lookDirection);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
            return;
        projectileHandler.projectile = prefabs[Random.Range(0, prefabs.Length)];
        projectileHandler.SpreadAngle = 5;
        projectileHandler.ProjectileCount = 60;
        onTriggerStay = false;
    }



    public override void Birth()
    {
        base.Birth();
        animationHandler.AttackHandler.OnEnter += () => projectileHandler.RangeAttack(transform.position, lookDirection);
    }

    protected override void HandleLogic()
    {
        base.HandleLogic();
        lookDirection = (Managers.Game.Player.transform.position - transform.position).normalized;
        if (onTriggerStay)
        {
            moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = lookDirection;
        }
    }

}