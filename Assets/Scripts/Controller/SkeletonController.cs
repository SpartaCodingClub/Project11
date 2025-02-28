using DG.Tweening;
using TMPro;
using UnityEngine;

public class SkeletonController : ObjectController
{
    private bool onTriggerStay;
    private ProjectileHandler projectileHandler;
    private StatHandler statHandler;
    private float time = 0f;
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
        projectileHandler.ProjectileCount = 10;
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
        animationHandler.AttackHandler.OnEnter += () =>
        {
            projectileHandler.RangeAttack(transform.position, lookDirection);
        };
    }

    protected override void HandleLogic()
    {
        base.HandleLogic();

        if (IsDead)
            return;

        if (Managers.Game.Player == null)
            return;


        lookDirection = (Managers.Game.Player.transform.position - transform.position).normalized;

        if (onTriggerStay)
        {
            moveDirection = Vector2.zero;
        }
        else
        {
            Vector2 perpendicularDiretion = Vector2.Perpendicular(lookDirection);
            moveDirection = lookDirection * 0.5f + perpendicularDiretion * 0.5f;

            float distance = Vector2.Distance(transform.position, Managers.Game.Player.transform.position);
            if (distance > statHandler.AttackRange * 1.5f)
            {
                moveDirection += lookDirection * 0.5f;
                statHandler.MoveSpeed = 3f;

            }
            else if (distance < statHandler.AttackRange * 1.2f)
            {
                moveDirection -= lookDirection * 0.5f;
                statHandler.MoveSpeed = 1f;
            }
            moveDirection.Normalize();
            _rigidbody.velocity = moveDirection * statHandler.MoveSpeed;
        }
        time += Time.deltaTime;
        if(time > 5 && onTriggerStay)
        {
            Shout();
            time = 0;
        }

    }

    private void Shout()
    {
        Vector2 direction = (Managers.Game.Player.transform.position - transform.position).normalized;
        Vector2 pushDirection = direction * 2f;
        Collider2D playerCollider = Managers.Game.Player.GetComponent<Collider2D>();
        if(playerCollider != null )
        {
            GameObject effect = Managers.Resource.Instantiate("JumpEffect", null, transform.position, Define.EFFECT);
            SpriteRenderer sr = effect.GetComponentInChildren<SpriteRenderer>();;
            effect.transform.DOScale(5, 1f).SetEase(Ease.OutBack).OnComplete(() => Destroy(effect));
            animationHandler.Animator.SetTrigger("Boost");
            playerCollider.attachedRigidbody.position += (Vector2)pushDirection;
            Vector2 pushDirectionWithReflection = direction * 2f;
            playerCollider.attachedRigidbody.velocity = pushDirectionWithReflection;
        }
    }

}