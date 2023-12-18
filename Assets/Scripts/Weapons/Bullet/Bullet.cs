using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rbBullet;
    private CircleCollider2D circleCollider;
    private float _shootingPower;
    private Vector3 _direction;
    private float _impactPower;
    private bool shootMe = false;
    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {
        if (shootMe)
        {
            rbBullet.AddForce(_direction.normalized * _shootingPower, ForceMode2D.Impulse);
        }
    }

    public void ShootMe(float shootingPower, float impactPower, Vector3 direction)
    {
        _direction = direction;
        _shootingPower = shootingPower;
        _impactPower = impactPower;
        shootMe = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ForceablePlatform")
        {
            shootMe = false;
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);

            circleCollider.enabled = false;
            StartCoroutine(DestroySelf());
        }



        
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        rbBullet.velocity = Vector2.zero;
        Destroy(gameObject);
    }


}
