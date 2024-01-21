using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public float[] spectrumWidth;

    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        spectrumWidth = new float[128];
        audioSource = GetComponent<AudioSource>();

        instance = this;
        
    }

    private void FixedUpdate()
    {
        audioSource.GetSpectrumData(spectrumWidth, 0, FFTWindow.Rectangular);
    }

    public float getFrequenciesDiapson(int start, int end, int mult)
    {
        return spectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }

}
