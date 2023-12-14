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

    private bool shootMe = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (shootMe)
        {
            rb.AddForce(_direction * _shootingPower, ForceMode2D.Impulse);
        }
    }

    public void ShootMe(float shootingPower, Vector3 direction)
    {
        _direction = direction;
        _shootingPower = shootingPower;
        shootMe = true;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "ForceablePlatform")
        {  
            Debug.Log("Trigger");
            shootMe = false;
            rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.right,collision.transform.position);
        }
        
    }
}
