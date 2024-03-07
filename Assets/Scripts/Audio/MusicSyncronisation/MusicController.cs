using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [SerializeField]
    private AudioSource healingAudioSource;

    [SerializeField]
    private AudioSource mainAudioSource;
    public float[] syncedMusicSpectrumWidth;


    public float transitionDurationTime = 2f;

    private bool isTransitioning = false;
    private bool isTransitioningMain = false;
    private bool isTransitioningHeal = false;
    private float transitionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        syncedMusicSpectrumWidth = new float[128];
        instance = this;
        
    }

    private void FixedUpdate()
    {
        if (!isTransitioning)
        {
            mainAudioSource.GetSpectrumData(syncedMusicSpectrumWidth, 0, FFTWindow.Rectangular);
        }
        else
        {
            if (isTransitioningHeal && healingAudioSource.volume < 0.8f)
            {
                UpdateMusicTransition(mainAudioSource, healingAudioSource, 0.5f);
                Debug.Log("isTransitioningHeal");
            }
            else if (isTransitioningMain && mainAudioSource.volume < 0.8f)
            {
                UpdateMusicTransition(healingAudioSource, mainAudioSource, 0.25f);
                Debug.Log("isTransitioningMain");
            }
        }

        

    }

    public float getFrequenciesDiapson(int start, int end, int mult)
    {
        return syncedMusicSpectrumWidth.ToList().GetRange(start, end).Average() * mult;
    }



    // Method to transition between two audio sources
    private void UpdateMusicTransition(AudioSource audioSourceToFadeOut, AudioSource audioSourceToFadeIn, float transitionDuration)
    {
       // audioSourceToFadeOut.Play();
       // audioSourceToFadeIn.Play();

        // Update transition timer
        transitionTimer += Time.deltaTime;

        // Calculate transition progress
        float progress = Mathf.Clamp01(transitionTimer / transitionDuration);

        // Fade out the audio source to fade out
        audioSourceToFadeOut.volume = Mathf.Lerp(0.8f, 0f, progress);

        // Fade in the audio source to fade in
        audioSourceToFadeIn.volume = Mathf.Lerp(0f, 0.8f, progress);

        // Check if transition is complete
        if (transitionTimer >= transitionDuration)
        {
            isTransitioning = false;
            isTransitioningHeal = false;
            isTransitioningMain = false;
            audioSourceToFadeOut.Stop();
            audioSourceToFadeIn.volume = 0.8f; // Ensure volume is set to desired level
            transitionTimer = 0f;
        }
    }

    // Method to transition to healing music
    public void TransitionToHealingMusic()
    {
        isTransitioning = true;
        isTransitioningHeal = true;
        isTransitioningMain = false;
        healingAudioSource.Play();
     
  
    }

    // Method to transition to main music
    public void TransitionToMainMusic()
    {
        isTransitioning = true;
        isTransitioningHeal = false;
        isTransitioningMain = true;
        mainAudioSource.Play();

    }
}


