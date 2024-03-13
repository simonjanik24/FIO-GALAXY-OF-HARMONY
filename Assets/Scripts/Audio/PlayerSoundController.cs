using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundController : MonoBehaviour
{

   
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> jumpSounds;
    [SerializeField]
    private List<AudioClip> dashSounds;
    [SerializeField]
    private AudioSource walkAudioSource;
    [SerializeField]
    private List<AudioClip> walkSounds;
    [SerializeField]
    private List<AudioClip> shootSounds;
    [SerializeField]
    private List<AudioClip> swingSounds;
    [SerializeField]
    private List<AudioClip> hurtSounds;
    [SerializeField]
    private List<AudioClip> onLandingSounds;
    [SerializeField]
    private float hurtSoundDelay = 1f;
    private bool canPlayHurtSound = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private AudioClip GetRandomSound(List<AudioClip> soundList)
    {
        if (soundList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, soundList.Count);
        return soundList[randomIndex];
    }

    public void PlayOnLandingSound()
    {
        AudioClip sound = GetRandomSound(onLandingSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }

    public void PlayJumpSound()
    {
        AudioClip sound = GetRandomSound(jumpSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }

    public void PlayDashSound()
    {
        AudioClip sound = GetRandomSound(dashSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }

   

    public void PlayHurtSound()
    {
        if (canPlayHurtSound)
        {
            AudioClip sound = GetRandomSound(hurtSounds);
            if (sound != null)
            {
                audioSource.PlayOneShot(sound);
                canPlayHurtSound = false; // Disable playing hurt sound temporarily
                StartCoroutine(EnableHurtSoundAfterDelay(hurtSoundDelay)); // Enable playing hurt sound after a delay
            }
        }
    }

    private IEnumerator EnableHurtSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlayHurtSound = true; // Re-enable playing hurt sound
    }

    public void PlayWalkSound()
    {
        if (!walkAudioSource.isPlaying)
        {
            AudioClip sound = GetRandomSound(walkSounds);
            if (sound != null)
            {
                walkAudioSource.clip = sound;
                walkAudioSource.loop = true;
                walkAudioSource.Play();
            }
        }
    }

    public void StopWalkSound()
    {
        walkAudioSource.Stop();
    }

    public void PlayShootSound()
    {
        AudioClip sound = GetRandomSound(shootSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }

    public void PlaySwingSound()
    {
        AudioClip sound = GetRandomSound(swingSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }


}

