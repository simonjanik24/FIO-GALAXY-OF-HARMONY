using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpPad : MonoBehaviour
{
    [Header("Inputs: Object")]
    [SerializeField]
    private float bouncePower = 1f;
    [SerializeField]
    private float bounceTime;

    [Header("Inputs: Animation")]
    [SerializeField]
    private Animator animator;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ExecuteBounce(collision));
        }

    }



    private IEnumerator ExecuteBounce(Collision2D collision)
    {
        Debug.Log("Bounce");
        animator.SetBool("IsBouncing", true);
        Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;    
        playerRigidbody.AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(bounceTime);
        animator.SetBool("IsBouncing", false);
       
    }

}
