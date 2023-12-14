using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _shootingPower;
    private Vector3 _direction;
    private float _impactPower;
    private bool shootMe = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (shootMe)
        {
            rb.AddForce(_direction.normalized * _shootingPower, ForceMode2D.Impulse);
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
            rb.velocity = Vector2.zero;

            Vector2 direction = _direction - collision.transform.position;

            Vector2 forceDirection = (_direction - collision.transform.position).normalized;



            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(forceDirection * _impactPower, collision.transform.position);
            Destroy(gameObject);
        }
        
    }
}
