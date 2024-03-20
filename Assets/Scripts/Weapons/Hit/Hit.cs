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
        Debug.Log("Is Hitting");
        if (playerController.IsHiting)
        {
            switch (collision.gameObject.tag)
            {
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
