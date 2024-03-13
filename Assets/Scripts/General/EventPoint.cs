using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class EventPoint : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider.enabled = false;
        unityEvent.Invoke();
        Destroy(gameObject);
    }
}
