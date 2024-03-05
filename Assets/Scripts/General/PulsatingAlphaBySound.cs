using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PulsatingAlphaToMusic : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private AudioSource targetMusic;
    [SerializeField]
    private SpriteRenderer sprite;
    [Header("Settings")]
    public float factor = 2;
    public float reactivness = 0.1f;
    [SerializeField]
    private bool bass = true;
    [SerializeField]
    private bool nb = true;
    [SerializeField]
    private bool middles = true;
    [SerializeField]
    private bool highs = true;
    [Header("What's going on?")]
    public float[] syncedMusicSpectrum;


    // Start is called before the first frame update
    void Start()
    {
        syncedMusicSpectrum = new float[1024];
    }

    private void FixedUpdate()
    {
        if (targetMusic != null)
        {
            targetMusic.GetSpectrumData(syncedMusicSpectrum, 0, FFTWindow.Rectangular);
        }

        if (bass)
        {
            float frequencyBass = getFrequenciesDiapson(0, 7, 10) * factor;
            PulsateAlpha(frequencyBass);
        }
        if (nb)
        {
            float frequencyNB = getFrequenciesDiapson(7, 15, 100) * factor;
            PulsateAlpha(frequencyNB);
        }
        if (middles)
        {
            float frequencyMiddles = getFrequenciesDiapson(15, 30, 200) * factor;
            PulsateAlpha(frequencyMiddles);
        }
        if (highs)
        {
            float frequencyHighs = getFrequenciesDiapson(30, 32, 1000) * factor;
            PulsateAlpha(frequencyHighs);
        }

    }

    private float getFrequenciesDiapson(int start, int end, int mult)
    {
        return syncedMusicSpectrum.ToList().GetRange(start, end).Average() * mult;
    }


    private void PulsateAlpha(float frequency)
    {
        float scaledFrequency = Mathf.Clamp(frequency, 0,1);
        Debug.Log(scaledFrequency);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, scaledFrequency);
    }
}
