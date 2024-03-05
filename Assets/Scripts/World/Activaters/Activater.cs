using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activater : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private List<string> tagsToReactTo;
    [SerializeField]
    private Sprite greenRecord;

    [Header("What's going on at runtime?")]
    [SerializeField]
    private bool activate = false;


    public bool IsActivate()
    {
        return activate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Col");
        if (CheckTags(collision))
        {
            activate = true;
            GetComponent<SpriteRenderer>().sprite = greenRecord;
            GetComponent<Animator>().SetBool("Reverse", true);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigg");
        if (CheckTags(collision))
        {
            activate = true;
            GetComponent<SpriteRenderer>().sprite = greenRecord;
            GetComponent<Animator>().SetBool("Reverse", true);

        }
    }


    private bool CheckTags(Collision2D collison)
    {
        bool isTag = false;

        foreach(string tag in tagsToReactTo)
        {
            if(collison.gameObject.tag == tag)
            {
                Debug.Log("Is Tag: "+ collison.gameObject.tag + " && " + tag);
                isTag = true;
                break;
            }
        }
        return isTag;
        
    }

    private bool CheckTags(Collider2D collison)
    {
        bool isTag = false;

        foreach (string tag in tagsToReactTo)
        {
            if (collison.gameObject.tag == tag)
            {
                Debug.Log("Is Tag collider: " + collison.gameObject.tag + " && " + tag);
                isTag = true;
                break;
            }
        }
        return isTag;

    }

}
