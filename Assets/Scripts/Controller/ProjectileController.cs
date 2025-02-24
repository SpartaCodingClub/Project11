using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    //private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration; //시간초과 체크
    private Vector2 direction;
    private bool isReady;
    

    private Rigidbody2D _rigidbody; //

    public bool fxOnDestory = true; //투사체가 삭제될때 이펙트 출력

    ProjectileManager ProjectileManager;
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
    //public void Init(Vector2 direction, RangeWeaponHandler RengeWeaponHandler, ProjectileManager projectileManager)
    //{
    //    this.ProjectileManager = projectileManager;

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
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}

