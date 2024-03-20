using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionEvent : MonoBehaviour
{
    [SerializeField]
    private bool waitBeforeEvent;
    [SerializeField]
    private float waitingTime;

    [SerializeField]
    private UnityEvent unityEvent;
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (waitBeforeEvent)
            {
                collider.enabled = false;
                StartCoroutine(WaitBeforeEvent());
            }
            else
            {
                collider.enabled = false;
                ExecuteEvent();
            }
        }
       
        
    }

    private IEnumerator WaitBeforeEvent()
    {
        yield return new WaitForSeconds(waitingTime);
        ExecuteEvent();
    }

    private void ExecuteEvent()
    {
        unityEvent.Invoke();
        Destroy(gameObject);
    }

  

}
