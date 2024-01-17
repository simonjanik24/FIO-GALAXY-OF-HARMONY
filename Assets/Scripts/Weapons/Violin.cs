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
    private Transform startPoint;



    public void FixedUpdate()
    {
        startPoint.position = Vector3.zero;
    }

    public void StartHovering()
    {
        violineAimingObjectHolder.SetActive(true);
        particleEffect.Play();
        particleEffect.SendEvent("ThickPlay");
        particleSystemBorder1.Play();
        particleSystemBorder2.Play();

    }

    public void StartMovingObject()
    {
        particleEffect.Play();
       
        
    }

    public void StopMovingObject()
    {
        particleEffect.Stop();
        particleEffect.SendEvent("ThickStop");
    }

    public void StopAll()
    {
        particleEffect.Stop();
        particleEffect.SendEvent("ThinStop");
        particleEffect.SendEvent("ThickStop");
        particleSystemBorder1.Stop();
        particleSystemBorder2.Stop();
        violineAimingObjectHolder.SetActive(false);
     
    }





    public override void Shoot(float shootingPower, float hitPower  )
    {
        throw new System.NotImplementedException();
    }


   
}
