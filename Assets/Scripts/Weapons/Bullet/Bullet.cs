using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DistanceToPlayerObserver))]
public class Bullet : MonoBehaviour
{
    private DistanceToPlayerObserver distanceToPlayerObserver;
    private Rigidbody2D rbBullet;
    private CircleCollider2D circleCollider;
    private float _shootingPower;
    private Vector3 _direction;
    private float _impactPower;
    private bool shootMe = false;

    private bool hitSomething = false;

    [SerializeField]
    private float destroyAfterTime;

    [SerializeField]
    private float currentTime;

    private void Awake()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        distanceToPlayerObserver = GetComponent<DistanceToPlayerObserver>();
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
        if(currentTime >= destroyAfterTime && hitSomething == false)
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
        if (distanceToPlayerObserver.IsInRange)
        {
            switch (collision.gameObject.tag)
            {
                case "Destructable":
                    circleCollider.enabled = false;
                    shootMe = false;
                    hitSomething = true;
                    //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);
                    collision.gameObject.GetComponent<Destructable>().DestroyMe();
                    StartCoroutine(DestroySelf());
                    break;
                case "ForceablePlatform":
                    shootMe = false;
                    hitSomething = true;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);
                    
                    circleCollider.enabled = false;
                    StartCoroutine(DestroySelf());
                    break;
            }
        } 
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        rbBullet.velocity = Vector2.zero;
        Destroy(gameObject);
    }


}
