using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ViolinSelectorObject : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1000;
    [SerializeField]
    private GameObject pathEnd;
    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField]
    private List<string> enabledTags;

    [SerializeField]
    private bool isHovering = false;

    private PlayerController playerController;

    

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    private void Update()
    {
  
        float step = 1000 * Time.deltaTime;
        pathEnd.transform.position = Vector3.MoveTowards(pathEnd.transform.position, transform.position, step);

      //  pathEnd.gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsTag(collision))
        {
            isHovering = true;

            if (collision.GetComponent<Moveable>())
            {
                collision.GetComponent<Moveable>().OnHover();

            }

            if (playerController.IsHoldingRightShoulder)
            {
                
                MoveObject(collision);
                if (collision.GetComponent<Moveable>())
                {
                    collision.GetComponent<Moveable>().OnSelect();

                }
            }
            else
            {
                visualEffect.SetFloat("Blend", 0);
                visualEffect.SetInt("SpawnRate", 10);
                visualEffect.SetVector2("Size", new Vector2(0.1f, 0.2f));
                if (collision.GetComponent<Moveable>())
                {
                    collision.GetComponent<Moveable>().OnDeselect();

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
                collision.GetComponent<Moveable>().OnHover();

            }
            if (playerController.IsHoldingRightShoulder)
            {
                MoveObject(collision);
                if (collision.GetComponent<Moveable>())
                {
                    collision.GetComponent<Moveable>().OnSelect();

                }
            }
            else
            {
                visualEffect.SetFloat("Blend", 0);
                visualEffect.SetInt("SpawnRate", 10);
                visualEffect.SetVector2("Size", new Vector2(0.1f, 0.2f));
                if (collision.GetComponent<Moveable>())
                {
                    collision.GetComponent<Moveable>().OnDeselect();

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
            if (collision.GetComponent<Moveable>())
            {
                collision.GetComponent<Moveable>().OnNothing();

            }


        }
        
    }

    private void MoveObject(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position);
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
