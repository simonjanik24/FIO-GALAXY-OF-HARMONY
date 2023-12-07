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
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(bounceTime);
        animator.SetBool("IsBouncing", false);
       
    }

}
