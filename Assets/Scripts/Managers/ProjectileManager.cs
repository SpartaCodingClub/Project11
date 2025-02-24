using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileManager
{
    public void Initialize()
    { 
        
    }

    //public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 derection)
    //{
    //    GameObject origin = projectilePrefab[rangeWeaponHandler.BulletIndex]; //불렛 인덱스값을 받아옴
    //    GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);  // 씬에 프리팹 생성

    //    ProjectileController projectileController = obj.GetComponent<ProjectileController>(); //프로젝타일 컨트롤러에서 컴퍼넌트 받아오기
    //    projectileController.Init(derection, rangeWeaponHandler, this); // 초기화
    //}
}