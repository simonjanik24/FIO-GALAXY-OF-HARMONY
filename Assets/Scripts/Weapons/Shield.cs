using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldDuration = 5f;// Standard shield duration in seconds
    [SerializeField]
    private List<ParticleSystem> particleSystems; // Array of particle systems inside the shield

    private bool isFlickering = false;



    private void Start()
    {
        Activate();
    }

    // Method to activate the shield
    public void Activate()
    {
        StartCoroutine(ShieldActivationRoutine());
    }

    // Coroutine for shield activation
    private IEnumerator ShieldActivationRoutine()
    {
        // Activate particle systems by default
        ActivateParticleSystems(true);

        yield return new WaitForSeconds(shieldDuration - 2f); // Wait for 2 seconds less than the total duration

        // Start flickering the shield
        isFlickering = true;
        StartCoroutine(FlickerRoutine());

        yield return new WaitForSeconds(2f); // Wait for the remaining 2 seconds

        // Deactivate the shield and stop flickering
        DeactivateShield();
    }

    // Coroutine for shield flickering
    private IEnumerator FlickerRoutine()
    {
        while (isFlickering)
        {
            yield return new WaitForSeconds(0.2f); // Flicker every 0.2 seconds
            ActivateParticleSystems(!particleSystems[0].isPlaying); // Toggle the state of particle systems
        }
    }

    // Method to activate or deactivate particle systems
    private void ActivateParticleSystems(bool activate)
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            if (activate)
                ps.Play();
            else
                ps.Stop();
        }
    }

    // Method to deactivate the shield
    private void DeactivateShield()
    {
        // Stop flickering
        isFlickering = false;

        // Deactivate particle systems
        ActivateParticleSystems(false);

        // Perform any additional cleanup or actions when the shield is deactivated
        Debug.Log("Shield deactivated");
    }
}
