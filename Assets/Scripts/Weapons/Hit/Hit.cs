using UnityEngine;

public class Hit : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerController.IsHiting)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    if (collision.gameObject.GetComponent<Enemy>())
                    {
                        collision.gameObject.GetComponent<Enemy>().DestroyMe();

                    }else if (collision.gameObject.GetComponent<EnemyBodyProxy>())
                    {
                        collision.gameObject.GetComponent<EnemyBodyProxy>().Enemy.DestroyMe();
                    }

                    

                    break;
                case "Destructable":
                    collision.gameObject.GetComponent<Destructable>().DestroyMe();

                    break;

                case "ForceablePlatform":
                    // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rbBullet.velocity.normalized * _impactPower, ForceMode2D.Impulse);


                    break;
            }
        }

       
    }
}
