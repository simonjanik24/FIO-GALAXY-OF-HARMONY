using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trompete : Weapon
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform rotator;
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private Transform target;

    public override void Shoot(float shootingPower)
    {

        // Calculate the direction towards the target
        Vector3 shootingDirection = (target.position - muzzle.position).normalized;

        float angle = Vector3.Angle(Vector3.right, shootingDirection);

        if (target.transform.position.y < transform.position.y) angle *= -1;

        //Create a rotation that will point towards the target
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Debug.Log(angle);
        Debug.Log(bulletRotation);

        // Calculate the rotation to face the target
        Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, shootingDirection);

        // Instantiate the bullet with the final rotation
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);

        // Shoot the bullet in the direction of the target
        bullet.GetComponent<Bullet>().ShootMe(shootingPower, shootingDirection);

    }
    
}
