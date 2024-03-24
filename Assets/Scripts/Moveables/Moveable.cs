using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Moveable : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private bool canPlayerMoveOnIt;
    [SerializeField]
    private bool moveOnX;
    [SerializeField]
    private bool moveOnY;
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private ParticleSystem hoverParticleSystems;
    [SerializeField]
    private ParticleSystem selectedParticleSystem;


    private Rigidbody2D rigidbody;

    [Header("On Runtime")]
    [SerializeField]
    private bool isMovingByPlayer = false;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private Vector2 currentPlayerPosition;

    public bool CanPlayerMove { get => canPlayerMoveOnIt; set => canPlayerMoveOnIt = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "  " + collision.gameObject.layer);
        // Check if the collision is with the ground layer (layer 3 or 6)

        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {


            // Stop the object from falling
            Debug.Log("Stay On Ground");
            isGrounded = true;
            isMovingByPlayer = false;
            rigidbody.bodyType = RigidbodyType2D.Static;
        }

        if (collision.gameObject.name == "Player" && canPlayerMoveOnIt) // 16 = Player
        {
            collision.gameObject.transform.SetParent(transform);

            currentPlayerPosition = collision.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player" && canPlayerMoveOnIt)  // 16 = Player
        {
            collision.gameObject.transform.SetParent(transform);

            if (isMovingByPlayer)
            {
                Vector3 playerColPos = collision.gameObject.transform.position;
                Vector3 playerLocalColPos = collision.gameObject.transform.localPosition;
                Vector3 newPosition = playerColPos;
                if (moveOnX & moveOnY)
                {
                    newPosition = new Vector3(transform.position.x, transform.position.y,playerColPos.z); 
                }
                else if (moveOnX)
                {
                    newPosition = new Vector3(transform.position.x, playerColPos.y, playerColPos.z);
                }
                else if (moveOnY)
                {
                    newPosition = new Vector3(playerColPos.x, transform.position.y, playerColPos.z);
                }
 
                collision.gameObject.transform.position = Vector3.MoveTowards(
                            playerColPos,
                            newPosition,
                            Time.deltaTime * moveSpeed);
            }
                
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player" && canPlayerMoveOnIt) // 16 = Player
        {
            collision.gameObject.transform.SetParent(null);
            currentPlayerPosition = Vector2.zero;
        }
    }

    public void OnSelect()
    {
        isMovingByPlayer = true;
        selectedParticleSystem.Play();
        
    }

    public void OnDeselect()
    {
        isMovingByPlayer = false;
        selectedParticleSystem.Stop();
        if (!isGrounded)
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.gravityScale = 200;
            rigidbody.mass = 20;
        }
        
    }

    public void OnHover()
    {
        isMovingByPlayer = false;
        hoverParticleSystems.Play();
    }

    public void OnMove(Vector2 position)
    {
        isMovingByPlayer = true;
        isGrounded = false;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.gravityScale = 0;
        rigidbody.mass = 0.5f;


        if (moveOnX && moveOnY)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * moveSpeed);

        } else if (moveOnX)
        {
            position.y = 0;
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * moveSpeed);

        } else if (moveOnY)
        {
            position.x = 0;
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * moveSpeed);
        }
        
    }

    public void OnUnhover()
    {
        isMovingByPlayer = false;
        hoverParticleSystems.Stop();
    }


    public void OnNothing()
    {
        isMovingByPlayer = false;
        selectedParticleSystem.Stop();
        hoverParticleSystems.Stop();
    }



}
