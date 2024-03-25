using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))] 
public class SequenceController : MonoBehaviour
{
    private Animator animator;

    private bool isChild = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (isChild)
        {
            Detach();
        }
    }


    public void StopAnimator()
    {
        animator.StopPlayback();  
    }

    public void Detach()
    {
        foreach (Transform child in transform)
        {
            child.parent = null;
        }
        animator.enabled = false;

        if(transform.childCount > 0)
        {
            isChild = true;
        }
        else
        {
            isChild = false;
        }
        GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>().Level1Setup();
        Debug.Log("Detach");
     
    }


    public void DisablePlayerAnimator()
    {
        GameObject.Find("Player").transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
    }

    public void EnablePlayerAnimator()
    {
        GameObject.Find("Player").transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
    }


    public void StartLevelMusic()
    {
        GameObject.Find("Controllers").transform.Find("MusicController").gameObject.SetActive(true);
        
       // Debug.Log("Music");

    }

}
