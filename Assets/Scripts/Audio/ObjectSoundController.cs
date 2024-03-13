using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> sounds;



    public void PlaySound()
    {
        if(audioSource != null && sounds != null && sounds.Count > 0)
        {
            AudioClip sound = GetRandomSound(sounds);
            if (sound != null)
            {
                audioSource.PlayOneShot(sound);
            }   
        }
        
    }

    private AudioClip GetRandomSound(List<AudioClip> soundList)
    {
        if (soundList.Count == 0)
            return null;

        int randomIndex = Random.Range(0, soundList.Count);
        return soundList[randomIndex];
    }

}
