using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

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
        StartCoroutine(DestroySelf());
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(waitingTimeBeforeDestroy);
        Destroy(gameObject);
    }


}
