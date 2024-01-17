using PathCreation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class ViolinSelectorObject : MonoBehaviour
{
    [SerializeField]
    private GameObject pathEnd;
    [SerializeField]
    private VisualEffect particleSystem;

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
                particleSystem.SetFloat("Blend", 0);
                particleSystem.SetInt("Rate", 10);
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
                particleSystem.SetFloat("Blend", 0);
                particleSystem.SetInt("Rate", 10);
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
            particleSystem.SetFloat("Blend", 0);
            particleSystem.SetInt("Rate", 10);
            if (collision.GetComponent<Moveable>())
            {
                collision.GetComponent<Moveable>().OnNothing();

            }


        }
        
    }

    private void MoveObject(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position);
        particleSystem.SetFloat("Blend", 1);
        particleSystem.SetInt("Rate", 50);
 
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
