using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.gameObject.GetComponent<RespawnController>().CurrentRespawnPosition = collision.transform.position;
    }
}
