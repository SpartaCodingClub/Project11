using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public LayerMask target;

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }
    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get { return bulletSize; } }
    [SerializeField] private float bulletSpeed = 1f;
    public float BulletSpeed { get { return bulletSpeed; } }


    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }


    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }


    private void CreateProjectile()
    {
        //projectileController.ShootBullet(
            //this,projectileSpawnPosition.position, //공격하고있는방향좌표
    }
}
