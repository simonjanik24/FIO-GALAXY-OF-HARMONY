using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerSoundController))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(Flickering))]
public class PlayerCollisionDetector : MonoBehaviour
{
    private PlayerController playerController;
    private WeaponController weaponController;
    private HealthManager healthManager;
    private Flickering flickerController;
    private PlayerSoundController soundController;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
        healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
        flickerController = GetComponent<Flickering>();
        soundController = GetComponent<PlayerSoundController>();
    }


    private void OnParticleCollision(GameObject other)
    {

        ExcecuteCollisionBehavior(other);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExcecuteCollisionBehavior(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ExcecuteCollisionBehavior(collision.gameObject);
    }


    private void ExcecuteCollisionBehavior(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "EvilScream":
                if (weaponController.Shield.IsActive == false)
                {
                    healthManager.Damage(10);
                    flickerController.StartFlicker();
                    soundController.PlayHurtSound();
                }
                break;
            case "Enemy":
                if (weaponController.Shield.IsActive == false)
                {
                    healthManager.Damage(5);
                    flickerController.StartFlicker();
                    soundController.PlayHurtSound();
                }
                break;
        }
        switch (gameObject.layer)
        {
            case 6: //Dead Layer
                
                    Debug.Log("Hit Dead Layer");

                    healthManager.Damage(10);
                    flickerController.StartFlicker();
                    soundController.PlayHurtSound();
                break;

        }

    }



}
