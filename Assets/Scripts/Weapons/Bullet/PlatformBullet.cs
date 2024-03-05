using System.Collections;
using UnityEngine;

public class PlatformBullet : MonoBehaviour
{
    [SerializeField]
    private Transform keysParent;
    [SerializeField]
    private Vector2 randomVelocityRange;

    private float _shootingPower;
    private Vector3 _direction;
    private bool shootMe = false;

    [SerializeField]
    private float destroyAfterTime;

    [SerializeField]
    private float currentTime;

    private void FixedUpdate()
    {
        if (shootMe)
        {
            foreach(Transform key in keysParent)
            {
                key.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    _direction.normalized * _shootingPower *
                    Random.Range(randomVelocityRange.x, randomVelocityRange.y), ForceMode2D.Impulse);
            }
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (gameObject != null && currentTime >= destroyAfterTime)
        {
            Destroy(gameObject);
        }
    }

    public void ShootMe(float shootingPower, Vector3 direction)
    {
        _direction = direction;
        _shootingPower = shootingPower;
        shootMe = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "ForceablePlatform":

                break;

        }




    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        //rbBullet.velocity = Vector2.zero;
        Destroy(gameObject);
    }


}
