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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collider.enabled = false;
        unityEvent.Invoke();
        Destroy(gameObject);
    }
}
