using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter: " + collision.gameObject.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Stay: " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exit: " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter: " + collision.gameObject.name);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Stay: " + collision.gameObject.name);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit: " + collision.gameObject.name);
    }


}
