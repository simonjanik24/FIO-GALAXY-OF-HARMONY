using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScreen : MonoBehaviour
{
    [SerializeField]
  //  private Pulsating pulsatingController;
    public AudioClip heartbeatSound; // Reference to the heartbeat sound clip
    public AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField]
    private int lifePointsThreshold;


    // Method to adjust the alpha channel of the sprite based on current life points
    public void UpdateScreen(float currentLifePoints)
    {

        // Ensure the currentLifePoints is within the valid range of 0 to 100
        currentLifePoints = Mathf.Clamp(currentLifePoints, 0f, 100f);

        // Calculate alpha value based on current life points (assuming 0 is fully transparent and 100 is fully opaque)
        float alpha = 1f - (currentLifePoints / 100f);

        // If currentLifePoints reach or fall below 30, start pulsating effect
        if (currentLifePoints <= lifePointsThreshold)
        {
           // pulsatingController.StartPulsating();
            audioSource.PlayOneShot(heartbeatSound);
        }
        // If currentLifePoints rise above 30, stop pulsating effect
        else if (currentLifePoints > lifePointsThreshold )
        {
          //  pulsatingController.StopPulsating();
            audioSource.Stop();
        }
    }  



    public void ResetScreen()
    {
       
     //   pulsatingController.StopPulsating();
        audioSource.Stop();
        Debug.Log("Reset Damage Screen");     

    }
}
