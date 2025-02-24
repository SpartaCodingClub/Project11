using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private ProjectileHandler projectileHandler;

    private Vector2 direction;
    private bool isReady;


    private Rigidbody2D _rigidbody; //

    public bool fxOnDestory = true; //투사체가 삭제될때 이펙트 출력

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        _rigidbody.velocity = direction * projectileHandler.BulletSpeed;
    }
    public void Init(Vector2 direction, ProjectileHandler ProjectileHandler)
    {

        projectileHandler = ProjectileHandler;

        this.direction = direction;
        transform.localScale = Vector3.one * ProjectileHandler.BulletSize;


        isReady = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽에 충돌했을때 투사체 파괴
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
        // 적에게 충돌했을때 투사체 파괴
        else if (projectileHandler.target.value == (projectileHandler.target.value | (1 << collision.gameObject.layer)))
        {
            //ResourceController resourceController = collision.GetComponent<ResourceController>(); //스탯 핸들러에서? 정보를 받아옴
            //if (resourceController != null)
            //{
            //    resourceController.ChangeHealth(-projectileHandler.Power);
            //    if (projectileHandler.IsOnKnockBack)
            //    {
            //        BaseController controller = collision.GetComponent<BaseController>();
            //        if (controller != null)
            //        {
            //            controller.ApplyKnockback(transform, projectileHandler.KnockBackPower, projectileHandler.KnockbackTime);
            //        }
            //    }
            //}
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
    public void ShootBullet(ProjectileHandler projectileHandler, Vector2 startPosition, Vector2 derection)
    {
        //GameObject origin = projectilePrefab[projectileHandler.BulletIndex]; //불렛 인덱스값을 받아옴
        //GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);  // 씬에 프리팹 생성
    }
}

