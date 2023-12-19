using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private float shieldDuration; // Standard shield duration in seconds
    [SerializeField]
    private float flickerMaxTime;
    [SerializeField]
    private float flickerInterval;
    [SerializeField]
    private List<ParticleSystem> particleSystems; // Array of particle systems inside the shield

    [Header("What's going on at runtime?")]
    [SerializeField]
    public float currentDuration = 0;
    [SerializeField]
    private float flickerTimer;
    [SerializeField]
    private bool isParticleSystemActive = true;

    public bool IsActive { get => isActive; set => isActive = value; }

    private void Awake()
    {
        flickerTimer = flickerInterval;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (isActive)
        {
            currentDuration += Time.deltaTime; 

            if (currentDuration >= shieldDuration)
            {
                isActive = false;
                foreach (ParticleSystem particleSystem in particleSystems)
                {
                    if (particleSystem.isPlaying)
                    {
                        particleSystem.Stop();
                    }

                    ParticleSystem.MainModule main = particleSystem.main;
                    main.playOnAwake = false;
                    main.prewarm = false;
                    particleSystem.gameObject.SetActive(false);
                    GetComponent<CircleCollider2D>().enabled = false;
                }
             //   Debug.Log("End | Shield Duration: " + shieldDuration+ " | Current Duration: " + currentDuration);
            }
            else
            {
                if (currentDuration >= shieldDuration - flickerMaxTime)
                {
                      flickerTimer -= Time.deltaTime;
                   // Debug.Log(flickerTimer);

                    if(flickerTimer <= 0f)
                    {
                        ToggleParticles();
                        flickerTimer = flickerInterval;

                    }
                   

                   // Debug.Log("Flicker | Shield Duration: " + shieldDuration+ " | Current Duration: " + currentDuration+ " | FlickerTime: " + flickerTimer);
                }
                else
                {
                //    Debug.Log("Play | Shield Duration: " + shieldDuration + " | Current Duration: " + currentDuration + " | FlickerTime: " + flickerTimer);
                    foreach (ParticleSystem particleSystem in particleSystems)
                    {
                        particleSystem.gameObject.SetActive(true);
                        GetComponent<CircleCollider2D>().enabled = true;
                        if (!particleSystem.isPlaying)
                        {
                            
                            particleSystem.Play();
                        }
                    }
                    isParticleSystemActive = true;
                }
            }
        }
        else
        {
            currentDuration = 0;
            flickerTimer = flickerInterval;
        }
 
    }

    private void ToggleParticles()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if(particleSystem.name != particleSystems[0].name ||
                particleSystem.name != particleSystems[1].name ||
                particleSystem.name != particleSystems[2].name) {

                // Toggle the state of the Particle System
                if (isParticleSystemActive)
                {
                    ParticleSystem.MainModule main = particleSystem.main;
                    main.playOnAwake = true;
                    main.prewarm = true;
                    particleSystem.gameObject.SetActive(false);
                }
                else
                {
                    ParticleSystem.MainModule main = particleSystem.main;
                    main.playOnAwake = true;
                    main.prewarm = true;
                    particleSystem.gameObject.SetActive(true);
                }
            }
           
        }
        isParticleSystemActive = !isParticleSystemActive;
    }

    public void Activate()
    {
        isActive = true;
    }

}
