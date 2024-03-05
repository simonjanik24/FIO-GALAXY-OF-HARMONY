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


    private void Start()
    {
        
    }

    public void StartHovering()
    {
        violineAimingObjectHolder.SetActive(true);
        particleEffect.SendEvent("Play");
        particleEffect.Play();
        particleSystemBorder1.Play();
        particleSystemBorder2.Play();
        Debug.Log("Start Hovering");

    }

    public void StartMovingObject()
    {
        // particleEffect.Play();
        particleEffect.SendEvent("Play");
        Debug.Log("Start Moving");

    }

    public void StopMovingObject()
    {
        //particleEffect.Stop();
        particleEffect.SendEvent("Stop");
        Debug.Log("Stop Moving");
    }

    public void StopAll()
    {
        particleEffect.Stop();
        particleEffect.SendEvent("Stop");
        particleSystemBorder1.Stop();
        particleSystemBorder2.Stop();
        violineAimingObjectHolder.SetActive(false);
        Debug.Log("Stop All");

    }





    public override void Shoot(float shootingPower, float hitPower  )
    {
        throw new System.NotImplementedException();
    }


   
}
