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

    [SerializeField]
    private float destroyAfterTime;

    [SerializeField]
    private float currentTime;

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

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(gameObject != null && currentTime >= destroyAfterTime)
        {
            Destroy(gameObject);
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

        switch (collision.gameObject.tag)
        {
            case "ForceablePlatform":
                shootMe = false;

                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);

                circleCollider.enabled = false;
                StartCoroutine(DestroySelf());
                break;

            case "Destructable":
                circleCollider.enabled = false;
                shootMe = false;
                
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Destructable>().DestroyMe();
                StartCoroutine(DestroySelf());
                break;
        }



        
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        rbBullet.velocity = Vector2.zero;
        Destroy(gameObject);
    }


}
