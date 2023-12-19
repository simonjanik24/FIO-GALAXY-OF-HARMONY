using UnityEngine;

public class EvilScream : MonoBehaviour
{
    [SerializeField]
    private Shake shakeScript; 
    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private float activationTimer;
    [SerializeField]
    private float restTimer;
    [SerializeField]
    private float shakeStrength;
    [SerializeField]
    private bool isShakingActive;
   [SerializeField]
    private bool isActivated = false;



    private void Start()
    {
        if(shakeScript != null && isShakingActive)
        {
            shakeScript.Duration = activationTimer;
            shakeScript.Strength = shakeStrength;
        }
        
        ParticleSystem.MainModule main = particleSystem.main;
        main.loop = true;
    }


    private void Update()
    {
        if (!isActivated)
        {
            restTimer -= Time.deltaTime;

            if (restTimer <= 0f)
            {
                // Activate EvilMouth
                ActivateEvilMouth();

                // Set the activation timer
                activationTimer = 3f;

                isActivated = true;
            }
        }
        else
        {
            activationTimer -= Time.deltaTime;

            if (activationTimer <= 0f)
            {
                // Deactivate EvilMouth
                DeactivateEvilMouth();

                // Set the rest timer
                restTimer = 2f;

                isActivated = false;
            }
        }
    }

    private void ActivateEvilMouth()
    {
        // Play particles
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

        // Call ShakeMe method of the Shake script
        if (shakeScript != null && isShakingActive)
        {
            shakeScript.ShakeMe();
        }
    }

    private void DeactivateEvilMouth()
    {
        // Stop particles
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        if (shakeScript != null && isShakingActive)
        {
            shakeScript.StopShakeMe();
        }
    }
}