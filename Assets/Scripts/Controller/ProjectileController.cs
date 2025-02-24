using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    //private RangeWeaponHandler rangeWeaponHandler;

    //private float currentDuration; //시간초과 체크
    //private Vector2 direction;
    //private bool isReady;
    

    //private Rigidbody2D _rigidbody; //

    //public bool fxOnDestory = true; //투사체가 삭제될때 이펙트 출력

    //ProjectileManager ProjectileManager;
    //private void Update()
    //{
    //    if (!isReady)
    //    {
    //        return;
    //    }

    //    currentDuration += Time.deltaTime;

    //    if (currentDuration > rangeWeaponHandler.Duration) //투사체가 시간을 초과하면
    //    {
    //        DestroyProjectile(transform.position, false);
    //    }

    //    _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    //}
    //public void Init(Vector2 direction, RangeWeaponHandler RengeWeaponHandler)
    //{

    //    rangeWeaponHandler = RengeWeaponHandler;

    //    this.direction = direction;
    //    currentDuration = 0;
    //    transform.localScale = Vector3.one * RengeWeaponHandler.BulletSize;


    //    isReady = true;
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 벽에 충돌했을때 투사체 파괴
    //    if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
    //    {
    //        DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
    //    }
    //    // 적에게 충돌했을때 투사체 파괴
    //    else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
    //    {
    //        ResourceController resourceController = collision.GetComponent<ResourceController>();
    //        if (resourceController != null)
    //        {
    //            resourceController.ChangeHealth(-rangeWeaponHandler.Power);
    //            if (rangeWeaponHandler.IsOnKnockBack)
    //            {
    //                BaseController controller = collision.GetComponent<BaseController>();
    //                if (controller != null)
    //                {
    //                    controller.ApplyKnockback(transform, rangeWeaponHandler.KnockBackPower, rangeWeaponHandler.KnockbackTime);
    //                }
    //            }
    //        }
    //        DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
    //    }
    //}
    //private void DestroyProjectile(Vector3 position, bool createFx)
    //{
    //    Destroy(this.gameObject);
    //}
    //public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 derection)
    //{
    //    GameObject origin = projectilePrefab[rangeWeaponHandler.BulletIndex]; //불렛 인덱스값을 받아옴
    //    GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);  // 씬에 프리팹 생성

    //    ProjectileController projectileController = obj.GetComponent<ProjectileController>(); //프로젝타일 컨트롤러에서 컴퍼넌트 받아오기
    //    projectileController.Init(derection, rangeWeaponHandler, this); // 초기화
    //}
}

