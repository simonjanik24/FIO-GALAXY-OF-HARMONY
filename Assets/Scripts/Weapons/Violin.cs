using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Violin : Weapon
{
    [SerializeField]
    private VisualEffect particleEffect;
    [SerializeField]
    private ParticleSystem particleSystemBorder1;
    [SerializeField]
    private ParticleSystem particleSystemBorder2;
    [SerializeField]
    private GameObject violineAimingObjectHolder;
    [SerializeField]
    private MusicToVisualEffect musicToVisualEffect;


    private void Start()
    {
        musicToVisualEffect.enabled = false;
    }

    public void StartHovering()
    {
        violineAimingObjectHolder.SetActive(true);
        particleEffect.SendEvent("Play");
       // particleEffect.Play();
        particleSystemBorder1.Play();
        particleSystemBorder2.Play();
        Debug.Log("Start Hovering");
        musicToVisualEffect.enabled = false;

    }

    public void StartMovingObject()
    {
        // particleEffect.Play();
        particleEffect.SendEvent("Play");
        Debug.Log("Start Moving");
        musicToVisualEffect.enabled = true;

    }

    public void StopMovingObject()
    {
        //particleEffect.Stop();
        particleEffect.SendEvent("Stop");
        Debug.Log("Stop Moving");
        musicToVisualEffect.enabled = false;
    }

    public void StopAll()
    {
        particleEffect.Stop();
        particleEffect.SendEvent("Stop");
        particleSystemBorder1.Stop();
        particleSystemBorder2.Stop();
        violineAimingObjectHolder.SetActive(false);
        musicToVisualEffect.enabled = false;
        Debug.Log("Stop All");

    }





    public override void Shoot(float shootingPower, float hitPower  )
    {
        throw new System.NotImplementedException();
    }


   
}
