using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.ParticleSystem;

public class MusicToVisualEffect : MonoBehaviour
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
    private VisualEffect visualEffect;

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
    private bool noiseIntensity;
    [SerializeField]
    private bool noiseFrequency;

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

    private VisualEffect defaultVisualEffect;
    private MainModule defaultParticleSystemMain;




    // Start is called before the first frame update
    void Start()
    {

        defaultVisualEffect = visualEffect;
        syncedMusicSpectrumWidth = new float[128];
    }

    private void FixedUpdate()
    {

        if (isSyncedWithViolin)
        {
            MusicController.instance.ViolinAudioSource.GetSpectrumData(syncedMusicSpectrumWidth, channel, spectrumType);
            ChangeParticles();
        }
        else if (isSyncedWithMain)
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

    private void AdjustParticles(float _frequency)
    {


        if (noiseIntensity)
        {
            visualEffect.SetFloat("NoiseIntensity", _frequency);
        }
        else
        {
            visualEffect.SetFloat("NoiseIntensity", defaultVisualEffect.GetFloat("NoiseIntensity"));
        }

        if (noiseFrequency)
        {
            visualEffect.SetFloat("NoiseFrequency", _frequency);
        }
        else
        {
            visualEffect.SetFloat("NoiseFrequency", defaultVisualEffect.GetFloat("NoiseFrequency"));
        }



    }

    public float GetFrequenciesDiapson(int start, int end, int mult)
    {
        return syncedMusicSpectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }


}

