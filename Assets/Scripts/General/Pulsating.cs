using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsating : MonoBehaviour
{
    public List<SpriteRenderer> sprites;
    public float pulsateTime = 1f; // Time taken for one complete pulsation cycle
    public float targetAlpha = 0.5f; // Target alpha value during pulsation

    private float currentAlpha = 1f; // Current alpha value
    private bool isPulsating = false; // Flag to check if pulsation is ongoing
    private float pulsateTimer = 0f; // Timer for pulsation


    void Update()
    {
        // Check if pulsation is ongoing
        if (isPulsating)
        {
            if(sprites != null)
            {
                // Increment the timer
                pulsateTimer += Time.deltaTime;

                // Check if the pulsation cycle is complete
                if (pulsateTimer >= pulsateTime)
                {
                    isPulsating = false;
                    pulsateTimer = 0f;
                }
                else
                {
                    // Calculate current alpha value based on sine function
                    currentAlpha = Mathf.Lerp(1f, targetAlpha, Mathf.PingPong(pulsateTimer / pulsateTime, 1f));

                    foreach (SpriteRenderer sprite in sprites)
                    {
                        // Apply the alpha value to the sprite renderer
                        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentAlpha);
                    }
                }
                
            }
            
        }
    }

    // Method to start pulsation
    public void StartPulsating()
    {
        isPulsating = true;
    }

    // Method to stop pulsation
    public void StopPulsating()
    {
        isPulsating = false;
        pulsateTimer = 0f;
        foreach (SpriteRenderer sprite in sprites)
        {
            // Apply the alpha value to the sprite renderer
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }
    }

    // Method to set pulsation time
    public void SetPulsateTime(float time)
    {
        pulsateTime = time;
    }
}
