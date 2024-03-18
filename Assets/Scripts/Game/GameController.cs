using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Initial Animation At Start Of Level")]
    [SerializeField]
    private Animation initialAnimation;
    [SerializeField]
    private KeyCode startButton = KeyCode.Space;

    private bool canStartGame = false;

    void Start()
    {
        // Start initial animation
      //  initialAnimation.Play();
        // Disable player input during initial animation
        DisablePlayerInput();
    }

    void Update()
    {
     /*   // Check if the initial animation has finished playing
        if (!initialAnimation.isPlaying && !canStartGame)
        {
            // Enable player input once the animation is finished
            EnablePlayerInput();
            canStartGame = true;
        }

        // Check for button press to start gameplay
        if (canStartGame && Input.GetKeyDown(startButton))
        {
            StartGameplay();
        }*/
    }

    void DisablePlayerInput()
    {
        // Disable player input logic here
        // For example:
        // GetComponent<PlayerController>().enabled = false;
    }

    void EnablePlayerInput()
    {
        // Enable player input logic here
        // For example:
        // GetComponent<PlayerController>().enabled = true;
    }

    void StartGameplay()
    {
        // Start main gameplay logic here
    }
}
