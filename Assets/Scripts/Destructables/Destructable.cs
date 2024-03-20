using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Destructable : MonoBehaviour
{
    [Header("Drop a surprise when destroyed")]
    [SerializeField]
    private GameObject surprise;
    [SerializeField]
    private bool hasGravity;
    [SerializeField]
    private float mass;
    [SerializeField]
    private float gravity;

    [Header("Inputs: GameObjects")]
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Transform particleSystems;
    [SerializeField]
    private float waitingTimeBeforeDestroy = 3f;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> destroyAudioClips;


    [Header("What's going on at runtime?")]
    [SerializeField]
    private float currentLifePoints;

    public void DestroyMe()
    {
        audioSource.clip = GetRandomSound(destroyAudioClips);
        audioSource.Play();
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


            if (hasGravity && _suprise.GetComponent<Rigidbody2D>())
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



    private AudioClip GetRandomSound(List<AudioClip> soundList)
    {
        if (soundList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, soundList.Count);
        return soundList[randomIndex];
    }

}
