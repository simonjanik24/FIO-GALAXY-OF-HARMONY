using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hoverParticleSystems;
    [SerializeField]
    private ParticleSystem selectedParticleSystem;



    public void OnSelect()
    {

        selectedParticleSystem.Play();
        
    }


    public void OnDeselect()
    {

        selectedParticleSystem.Stop();
        
    }

    public void OnHover()
    {
        hoverParticleSystems.Play();
    }

    public void OnUnhover()
    {
        hoverParticleSystems.Stop();
    }


    public void OnNothing()
    {
        selectedParticleSystem.Stop();
        hoverParticleSystems.Stop();
    }
}
