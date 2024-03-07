using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("What's going on at runtime?")]
    [SerializeField]
    private Vector3 currentRespawnPosition;

    public Vector3 CurrentRespawnPosition { get => currentRespawnPosition; set => currentRespawnPosition = value; }

   public void Respawn()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = currentRespawnPosition;
        player.GetComponent<Flickering>().StopFlicker();
        GameObject.Find("Main Camera").GetComponent<CameraController>().ResetPosition();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Respawn();
        }
    }
}
