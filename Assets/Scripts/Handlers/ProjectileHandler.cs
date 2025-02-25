using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public LayerMask target;
    private Rigidbody2D _rigidbody;
    private ProjectileController projectilecontroller;

    [Header("Ranged Attack Data")]

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }
    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get { return bulletSize; } }
    [SerializeField] private float bulletSpeed = 1f;
    public float BulletSpeed { get { return bulletSpeed; } }

    [SerializeField] private float bulletSpread; // 탄 퍼짐 정도
    public float BulletSpread { get { return bulletSpread; } }

    [SerializeField] private float multileProjectileAngle; // 탄 퍼짐 각도
    public float MultileProjectileAngle { get { return multileProjectileAngle; } }

    [SerializeField] private int numberofProjectilesPerShot; // 1번 발사할때 나가는 투사체 수
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    public void RangeAttack()
    {
        float projectileAngleSpace = multileProjectileAngle;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;

        float minAlge = -(numberOfProjectilesPerShot / 2f) * projectileAngleSpace;

        for (int i = 0; i < numberofProjectilesPerShot; i++)
        {
            //각도는 각도간격 * 투사체수
            float angle = minAlge * projectileAngleSpace * i;
            float Randomspread = Random.Range(-bulletSpread, bulletSpread);
            angle += Randomspread;
            //projectilecontroller.Fire(lookDirection, angle); //
        } 
    }
}
