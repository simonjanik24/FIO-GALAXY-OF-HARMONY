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
        particleEffect.Play();
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
    }

    public void StopAll()
    {
        particleEffect.Stop();
        particleSystemBorder1.Stop();
        particleSystemBorder2.Stop();
        violineAimingObjectHolder.SetActive(false);
     
    }





    public override void Shoot(float shootingPower, float hitPower  )
    {
        throw new System.NotImplementedException();
    }


   
}
