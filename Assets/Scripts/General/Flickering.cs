using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    public List<SpriteRenderer> sprites; // Reference to the sprite renderer component
    public float flickerDuration = 0.5f; // Duration of flickering
    public float flickerInterval = 0.1f; // Interval between flickers
    public float flickerResetTimer = 0f; // Delay before flickering starts after collision
    private bool isFlickering = false; // Flag to track if flickering is in progress
    private float flickerTimer = 0f; // Timer for flickering

    private void Update()
    {
        // Check if flickering is in progress
        if (isFlickering)
        {
            // Update the flicker timer
            flickerTimer += Time.deltaTime;
            flickerResetTimer += Time.deltaTime;

            // Check if flicker duration is over
            if (flickerTimer >= flickerDuration)
            {
                // End flickering
                isFlickering = false;
                // Ensure sprite is visible after flicker ends
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.enabled = true;
                }
            }
            else
            {

                // Check if it's time to toggle the sprite visibility
                if (flickerResetTimer >= flickerInterval)
                {
                    foreach (SpriteRenderer sprite in sprites)
                    {
                        sprite.enabled = !sprite.enabled;
                    }
                    flickerResetTimer = 0f;
                }
            }
        }
    }

    public void StartFlicker()
    {
        // Reset flicker timer and enable flickering
        flickerTimer = 0f;
        isFlickering = true;
    }

    public void StopFlicker()
    {
        // Reset flicker timer and enable flickering
        flickerTimer = 0f;
        isFlickering = false;
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
        }
    }
}
