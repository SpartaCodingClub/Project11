using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    [SerializeField] private LayerMask levelCollisionLayer;

    [SerializeField] private GameObject[] projectilePrefab;

    [SerializeField] private Transform projectileSpawnPos;

    private ProjectileHandler projectileHandler;

    protected Vector2 lookDirection;
    protected Vector2 direction;
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
    public void CreateProjectile(Vector2 startPosition, Vector2 derection)
    {
        //GameObject original = projectilePrefab[BulletIndex]; //불렛 인덱스값을 받아옴 
        //GameObject obj = Instantiate(original, startPosition, Quaternion.identity);  // 오브젝트 생성
    }
    public void Fire(Vector2 lookDirection, float angle)
    {
        //CreateProjectile(this, projectileSpawnPos.position, Rotation(lookDirection, angle)); //보는 방향에서 생성 ,  보는 방향으로 진행
    }
}

