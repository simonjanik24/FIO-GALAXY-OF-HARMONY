using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [Header("Inputs")]
    [SerializeField]
    private float violineTransitionDuration;
    [SerializeField]
    private float healTransitionDuration;
    [SerializeField]
    private float mainTransitionDuration;


    [SerializeField]
    private AudioSource violinAudioSource;
    [SerializeField]
    private List<AudioClip> violinAudioClips;
    [SerializeField]
    private AudioSource healingAudioSource;
    [SerializeField]
    private AudioSource mainAudioSource;
    
    public float[] syncedMusicSpectrumWidth;


    [Header("What's going on at runtime?")]
    [SerializeField]
    private float mainStartVolume;
    [SerializeField]
    private float healingStartVolume;
    [SerializeField]
    private float violineStartVolume;
    [SerializeField]
    private float targetVolume;

    private float lastVolume;

    [SerializeField]
    private bool isTransitioning = false;
    [SerializeField]
    private bool isTransitioningMain = false;
    [SerializeField]
    private bool isTransitioningHeal = false;
    [SerializeField]
    private bool isTransitioningViolin = false;
    [SerializeField]
    private float transitionTimer = 0f;

    public AudioSource ViolinAudioSource { get => violinAudioSource; set => violinAudioSource = value; }
    public AudioSource MainAudioSource { get => mainAudioSource; set => mainAudioSource = value; }
    public bool IsTransitioning { get => isTransitioning; set => isTransitioning = value; }


    // Start is called before the first frame update
    void Start()
    {
        mainStartVolume = mainAudioSource.volume;
        healingStartVolume = healingAudioSource.volume;
        violineStartVolume = violinAudioSource.volume;
        healingAudioSource.volume = 0;
        violinAudioSource.volume = 0;
       // Debug.Log(" Main Volume: " + mainStartVolume+ " Healing Volume: " + healingStartVolume + " Violin Volume: " + violineStartVolume);
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
            if (isTransitioningMain && mainAudioSource.volume < mainStartVolume)
              {
                  if (healingAudioSource.isPlaying)
                  {
                      UpdateMusicTransition(healingAudioSource, mainAudioSource, mainTransitionDuration);
                  }
                  else if (violinAudioSource.isPlaying)
                  {
                      UpdateMusicTransition(violinAudioSource, mainAudioSource, mainTransitionDuration);
                  }
            }
            else if (isTransitioningHeal && healingAudioSource.volume < healingStartVolume)
            {
                UpdateMusicTransition(mainAudioSource, healingAudioSource, healTransitionDuration);
            }
            else if (isTransitioningViolin && violinAudioSource.volume < violineStartVolume)
            {
                UpdateMusicTransition(mainAudioSource, violinAudioSource, violineTransitionDuration);
  
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
        
        // Update transition timer
        transitionTimer += Time.deltaTime;

        // Calculate transition progress
        float progress = Mathf.Clamp01(transitionTimer / transitionDuration);

        // Fade out the audio source to fade out
        audioSourceToFadeOut.volume = Mathf.Lerp(audioSourceToFadeOut.volume, 0f, progress);

        // Fade in the audio source to fade in
        audioSourceToFadeIn.volume = Mathf.Lerp(0f, targetVolume, progress);

      /*  Debug.Log("Transition from: " + audioSourceToFadeOut.gameObject.name + "  - to: " + audioSourceToFadeIn.gameObject.name +
          " in Time of " + transitionDuration + " | Current: " + transitionTimer);
      */

        // Check if transition is complete
        if (transitionTimer >= transitionDuration)
        {
            isTransitioning = false;
            isTransitioningHeal = false;
            isTransitioningMain = false;
            isTransitioningViolin = false;
            audioSourceToFadeOut.Pause();
            audioSourceToFadeIn.volume = targetVolume; // Ensure volume is set to desired level
            transitionTimer = 0f;
            /*
            Debug.Log("Finish Transition from: " + audioSourceToFadeOut.gameObject.name + "  - to: " + audioSourceToFadeIn.gameObject.name +
         " in Time of " + transitionDuration + " | Current: " + transitionTimer);
            */

        }


    }

    // Method to transition to healing music
    public void TransitionToHealingMusic()
    {
        isTransitioning = true;
        isTransitioningHeal = true;
        isTransitioningMain = false;
        isTransitioningViolin = false;
        targetVolume = GetTargetVolumeBy(healingAudioSource);
        healingAudioSource.Play();
     
  
    }

    // Method to transition to main music
    public void TransitionToMainMusic()
    {
        isTransitioning = true;
        isTransitioningHeal = false;
        isTransitioningMain = true;
        isTransitioningViolin = false;
        targetVolume = GetTargetVolumeBy(mainAudioSource);
        mainAudioSource.Play();

    }


    public void TransiationToViolinMusic()
    {
        isTransitioning = true;
        isTransitioningHeal = false;
        isTransitioningMain = false;
        isTransitioningViolin = true;
        targetVolume = GetTargetVolumeBy(violinAudioSource);
        violinAudioSource.clip = GetRandomAudioClip(violinAudioClips);
        violinAudioSource.Play();
    }


    private AudioClip GetRandomAudioClip(List<AudioClip> audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Count)];
    }

    private float GetTargetVolumeBy(AudioSource audioSource)
    {
        float volume = 0;

        if(audioSource == mainAudioSource)
        {
            volume = mainStartVolume;
        }
        else if(audioSource == healingAudioSource) { 
        
            volume = healingStartVolume;
        }
        else if(audioSource == violinAudioSource) {
            volume = violineStartVolume;
        }

        return volume;
    }


    public void TurnOn()
    {
        violinAudioSource.Play();
        healingAudioSource.Play();
        mainAudioSource.Play();

    }

    public void TurnOff()
    {
        violinAudioSource.Pause();
        healingAudioSource.Pause();
        mainAudioSource.Pause();
    }

 
}


