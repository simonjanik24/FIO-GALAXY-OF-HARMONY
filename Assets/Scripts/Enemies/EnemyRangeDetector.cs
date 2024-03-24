using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyRangeDetector : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemy.PlayerInRange = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerInRange = false;
        }

    }
}
