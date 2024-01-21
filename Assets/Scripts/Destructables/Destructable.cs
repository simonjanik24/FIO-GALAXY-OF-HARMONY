using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [Header("Drop a surprise when destroyed")]
    [SerializeField]
    private GameObject surprise;
    [SerializeField]
    private bool isGravity;
    [SerializeField]
    private float mass;
    [SerializeField]
    private float gravity;


    [Header("Inputs: Points")]
    [SerializeField]
    private float maxLifePoints;
    [SerializeField]
    private float damagePoints;
    [SerializeField]
    private float waitingTimeBeforeDestroy = 3f;

    [Header("Inputs: GameObjects")]
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Transform particleSystems;


    [Header("What's going on at runtime?")]
    [SerializeField]
    private float currentLifePoints;

   public void DestroyMe()
    {
        cube.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        foreach(Transform child in particleSystems)
        {
            child.gameObject.SetActive(true);
        }
        if(surprise != null)
        {
            GameObject _suprise = GameObject.Instantiate(surprise);
            _suprise.transform.parent = null;
            _suprise.transform.position = gameObject.transform.position;

            if (isGravity && _suprise.GetComponent<Rigidbody2D>())
            {
                _suprise.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                _suprise.GetComponent<Rigidbody2D>().mass = mass;
                _suprise.GetComponent<Rigidbody2D>().gravityScale = gravity;
                
            }

            //Instantiate(surprise);
        }
        
        StartCoroutine(DestroySelf());
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(waitingTimeBeforeDestroy);
        
        Destroy(gameObject);
    }


}
