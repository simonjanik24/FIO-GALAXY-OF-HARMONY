using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FragmentHarmonicSymphony : Collectable
{
    [SerializeField]
    private float waitingTimeBeforeDestroy = 3f;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("It was the player!");
            CollectMe();
        }
    }

    public override void CollectMe()
    {
        if (GetComponent<CircleCollider2D>())
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if (GetComponent<Rigidbody2D>())
        {
            Destroy(GetComponent<Rigidbody2D>());
           
        }
        audioSource.clip = audioClip;
        audioSource.Play();
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        StartCoroutine(DestroySelf());
    }


    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(waitingTimeBeforeDestroy);

        Destroy(gameObject);
    }
}
