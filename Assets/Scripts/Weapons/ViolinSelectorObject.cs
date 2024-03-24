using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class ViolinSelectorObject : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private float normalMoveSpeed = 70;
    [SerializeField]
    private GameObject pathEnd;
    [SerializeField]
    private VisualEffect visualEffect;
    [SerializeField]
    private List<string> enabledTags;


    [Header("On Runtime")]
    [SerializeField]
    private bool isHovering = false;
    [SerializeField]
    private Moveable currentMovable;
    [SerializeField]
    private float currentSpeed;

    private PlayerController playerController;

    public Moveable CurrentMovable { get => currentMovable; set => currentMovable = value; }

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentSpeed = normalMoveSpeed;
        
    }

    private void Update()
    {
  
        float step = 1000 * Time.deltaTime;
        pathEnd.transform.position = Vector3.MoveTowards(pathEnd.transform.position, transform.position, step);

      //  pathEnd.gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position);
    }

    public void Move(Vector2 target)
    {
        if (currentMovable != null)
        {
            currentSpeed = currentMovable.MoveSpeed;
            //Player is not on a movable platform
            if (currentMovable.CanPlayerMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
            }
            //Player is on a movable platform
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
            }

            Debug.Log("CurrentMovable: " + currentMovable.gameObject.name + " | MovableSpeed: " + currentMovable.MoveSpeed + " | Speed: " + currentSpeed);
        }
        else
        {
            currentSpeed = normalMoveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, target, currentSpeed);

            Debug.Log("No Current Movable | Speed: " + currentSpeed);
        }

     

        
    }

    public void Reset()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsTag(collision))
        {
            isHovering = true;

            if (collision.GetComponent<Moveable>())
            {
                currentMovable = collision.GetComponent<Moveable>();
            }
            
            if(currentMovable != null)
            {
                currentMovable.OnHover();
            }

            if (playerController.IsHoldingRightShoulder)
            {
                if (currentMovable != null)
                {
                    MoveObject(currentMovable);
                    currentMovable.OnSelect();
                }
            }
            else
            {
                visualEffect.SetFloat("Blend", 0);
                visualEffect.SetInt("SpawnRate", 10);
                visualEffect.SetVector2("Size", new Vector2(0.1f, 0.2f));
                if (currentMovable != null)
                {
                    currentMovable.OnDeselect();
                }
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsTag(collision))
        {
            isHovering = true;

            if (collision.GetComponent<Moveable>())
            {
                currentMovable = collision.GetComponent<Moveable>();
            }

            if (currentMovable != null)
            {
                currentMovable.OnHover();

            }
            if (playerController.IsHoldingRightShoulder)
            {
                

                if (currentMovable != null)
                {
                    MoveObject(currentMovable);
                    currentMovable.OnSelect();

                }
            }
            else
            {
                visualEffect.SetFloat("Blend", 0);
                visualEffect.SetInt("SpawnRate", 10);
                visualEffect.SetVector2("Size", new Vector2(0.1f, 0.2f));
                if (currentMovable != null)
                {
                    currentMovable.OnDeselect();

                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsTag(collision))
        {
            isHovering = false;
            visualEffect.SetFloat("Blend", 0);
            visualEffect.SetInt("SpawnRate", 10);
            visualEffect.SetVector2("Size", new Vector2(0.1f,0.2f));

            if (currentMovable != null)
            {
                currentMovable.OnNothing();

            }

            currentMovable = null;
        }
        
    }

    private void MoveObject(Moveable moveable)
    {
        moveable.OnMove(transform.position);
        visualEffect.SetFloat("Blend", 1);
        visualEffect.SetInt("SpawnRate", 50);
        visualEffect.SetVector2("Size", new Vector2(0.1f, 0.5f));

    }


    private bool IsTag(Collider2D collider)
    {
        bool isTag = false;

        foreach(string tag in enabledTags)
        {
            if (tag == collider.gameObject.tag)
            {
                isTag = true;
                break;
            }
        }
        return isTag;
    }


}
