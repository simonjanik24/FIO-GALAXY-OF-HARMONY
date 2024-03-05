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
    private List<AudioClip> walkSounds;
    [SerializeField]
    private List<AudioClip> shootSounds;
    [SerializeField]
    private List<AudioClip> swingSounds;
    [SerializeField]
    private List<AudioClip> hurtSounds;




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
        AudioClip sound = GetRandomSound(hurtSounds);
        if (sound != null)
            audioSource.PlayOneShot(sound);
    }

    public void PlayWalkSound()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip sound = GetRandomSound(walkSounds);
            if (sound != null)
            {
                audioSource.clip = sound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void StopWalkSound()
    {
        audioSource.Stop();
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

