using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaknessPoint : MonoBehaviour
{

    [Header("Inputs")]
    [SerializeField]
    private float bouncePower;

    [SerializeField]
    private Enemy enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ExecuteBounce(collision);
            enemy.DestroyMe();
            Destroy(gameObject);

        }
    }

    private void ExecuteBounce(Collision2D collision)
    {

        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);


    }

}
