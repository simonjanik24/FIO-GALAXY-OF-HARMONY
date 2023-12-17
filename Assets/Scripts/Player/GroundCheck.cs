using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded = false;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask deadLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == groundLayer || collision.gameObject.layer == deadLayer)
        {
            isGrounded = true;
            Debug.Log("Trigger Enter");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer || collision.gameObject.layer == deadLayer)
        {
            isGrounded = true;
            Debug.Log("Trigger Stay");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        Debug.Log("Trigger Exit");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
    }

    public bool IsGrounded()
    {
     //   Debug.Log("IsGrounded: " + isGrounded);

        return isGrounded;
    }



}
