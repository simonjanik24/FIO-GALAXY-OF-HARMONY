using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField]
    private ParticleSystem walkingParticles;
    [Range(0, 10)]
    [SerializeField]
    private int occurAfterVelocity;
    [Range(0, 0.2f)]
    [SerializeField]
    private float dustFormationPeriod;

    [Header("Landing")]
    [SerializeField]
    private ParticleSystem landingParticles;

    private float counter;
    private Rigidbody2D playerRigidBody2D;
    private PlayerController playerController;
    private bool wasJumping;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody2D = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (!playerController.IsGrounded())
        {
            wasJumping = true;
        }
        else
        {
            if (wasJumping)
            {
                landingParticles.Play();
                wasJumping = false;
            }
        }
  
        if(playerController.IsGrounded() && Mathf.Abs(playerRigidBody2D.velocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                walkingParticles.Play();
                counter = 0;
            }
        }
        
    }


}
