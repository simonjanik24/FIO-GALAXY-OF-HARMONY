using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : Weapon
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private Transform target;


    public override void Shoot(float shootingPower, float impactPower)
    {

        // Calculate the direction towards the target
        Vector3 shootingDirection = (target.position - muzzle.position).normalized;

        float angle = Vector3.Angle(Vector3.right, shootingDirection);

        if (target.transform.position.y < transform.position.y) angle *= -1;

        // Instantiate the bullet with the final rotation
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);

        // Shoot the bullet in the direction of the target
        bullet.GetComponent<PlatformBullet>().ShootMe(shootingPower, shootingDirection);

    }
}
