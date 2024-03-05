using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [SerializeField]
    private AudioSource healingMusic;

    [SerializeField]
    private AudioSource syncedMusic;
    public float[] syncedMusicSpectrumWidth;
    



    // Start is called before the first frame update
    void Start()
    {
        syncedMusicSpectrumWidth = new float[128];
        instance = this;
        
    }

    private void FixedUpdate()
    {
        if(syncedMusic != null) {
            syncedMusic.GetSpectrumData(syncedMusicSpectrumWidth, 0, FFTWindow.Rectangular);
        }
        
    }

    public float getFrequenciesDiapson(int start, int end, int mult)
    {
        return syncedMusicSpectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }

}
