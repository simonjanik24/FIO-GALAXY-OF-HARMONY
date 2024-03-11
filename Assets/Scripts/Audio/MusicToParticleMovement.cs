using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MusicToParticleMovement : MonoBehaviour
{

    [Header("Sync Scope")]
    [SerializeField]
    public bool isSyncedWithViolin;
    [SerializeField]
    public bool isSyncedWithMain;

    [Header("Inputs: Objects")]
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    private ParticleSystem particleSystem;

    [Header("Spectrum Data")]
    [SerializeField]
    private FFTWindow spectrumType = FFTWindow.Rectangular;
    [SerializeField]
    private int channel = 0;

    [Header("Frequency")]
    [SerializeField]
    private float factor = 2;

    
    

    [Header("Particle Impact")]
    [SerializeField]
    private bool lifetime;
    [SerializeField]
    private bool sizeMultiplier;
    [SerializeField]
    private bool rotation;
    [SerializeField]
    private bool gravity;
    [SerializeField]
    private bool speed;
    [SerializeField]
    private bool amount;
    [SerializeField]
    private bool noiseStrength;
    [SerializeField]
    private bool noiseFrequency;

    [Header("Particle Impact: Optional Settings")]
    [SerializeField]
    private float lifeTimeDivide = 2;
    [SerializeField]
    private float sizeDivide = 10;
    [SerializeField]
    private float amountMultiplier = 100;

    [Header("Audio Settings")]
    [SerializeField]
    private bool bass = true;
    [SerializeField]
    private bool nb = true;
    [SerializeField]
    private bool middles = true;
    [SerializeField]
    private bool highs = true;

    [SerializeField]
    private float[] syncedMusicSpectrumWidth;

    private ParticleSystem defaultParticleSystem;
    private MainModule defaultParticleSystemMain;




    // Start is called before the first frame update
    void Start()
    {
        defaultParticleSystemMain = particleSystem.main;
        defaultParticleSystem = particleSystem;
      syncedMusicSpectrumWidth = new float[128];
    }

    private void FixedUpdate()
    {

        if (isSyncedWithViolin)
        {
            MusicController.instance.ViolinAudioSource.GetSpectrumData(syncedMusicSpectrumWidth, channel, spectrumType);
            ChangeParticles();
        }else if (isSyncedWithMain)
        {
            MusicController.instance.MainAudioSource.GetSpectrumData(syncedMusicSpectrumWidth, channel, spectrumType);
            ChangeParticles();
        }
        else
        {
            audioSource.GetSpectrumData(syncedMusicSpectrumWidth, channel, spectrumType);
            ChangeParticles();
        }
        
      
       
    }


    private void ChangeParticles()
    {
        if (bass)
        {
            float frequencyBass = GetFrequenciesDiapson(0, 7, 10) * factor;
            AdjustParticles(frequencyBass);
        }
        if (nb)
        {
            float frequencyNB = GetFrequenciesDiapson(7, 15, 100) * factor;
            AdjustParticles(frequencyNB);
        }
        if (middles)
        {
            float frequencyMiddles = GetFrequenciesDiapson(15, 30, 200) * factor;
            AdjustParticles(frequencyMiddles);
        }
        if (highs)
        {
            float frequencyHighs = GetFrequenciesDiapson(30, 32, 1000) * factor;
            AdjustParticles(frequencyHighs);
        }
    }

    private void AdjustParticles(float frequency)
    {
        var main = particleSystem.main;

        if (lifetime)
        {
            main.startLifetime = new MinMaxCurve(frequency / lifeTimeDivide);
        }
        else
        {
            main.startLifetime = defaultParticleSystemMain.startLifetime;
        }

        if (sizeMultiplier)
        {
            main.startSize = main.startSize.constant * frequency / sizeDivide;
        }
        else
        {
            main.startSize= defaultParticleSystemMain.startSize;
        }

        if (rotation)
        {
            main.startRotation = frequency;
        }
        else
        {
            main.startRotation= defaultParticleSystemMain.startRotation;
        }

        if (gravity)
        {
            main.gravityModifier = frequency;
        }
        else
        {
            main.gravityModifier = defaultParticleSystemMain.gravityModifier;
        }

        if (speed)
        {
            main.startSpeed = frequency;
        }
        else
        {
           main.startSpeed = defaultParticleSystemMain.startSpeed;
        }

        if (amount)
        {
            main.maxParticles = Mathf.RoundToInt(main.maxParticles * frequency * amountMultiplier);
        }
        else
        {
           main.maxParticles = defaultParticleSystemMain.maxParticles;
        }


        if (noiseStrength)
        {
            var noiseModule = particleSystem.noise;
            noiseModule.strength = frequency;
        }
        else
        {
            var noiseModule = particleSystem.noise;
            var defaultNoiseModule = defaultParticleSystem.noise;
            noiseModule.strength = defaultNoiseModule.strength;

        }

        if (noiseFrequency)
        {
            var noiseModule = particleSystem.noise;
            noiseModule.frequency = frequency;
        }
        else
        {
            var noiseModule = particleSystem.noise;
            var defaultNoiseModule = defaultParticleSystem.noise;
            noiseModule.strength = defaultNoiseModule.strength;

        }






    }

    public float GetFrequenciesDiapson(int start, int end, int mult)
    {
        return syncedMusicSpectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }


}
