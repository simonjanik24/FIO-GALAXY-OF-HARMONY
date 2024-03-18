using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// If the Collider was hit by the player it triggeres the animation. If the animation is finished the event gets triggered.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class CollisionAnimationEvent : MonoBehaviour
{

    [Header("1.On collision play: ")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AnimationClip animationClip;

    [Header("2.On animation is finished: ")]
    [SerializeField]
    private UnityEvent onAnimationComplete;

    private bool isAnimationPlaying = false;


    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Check if Animator and AnimationClip are provided
        if (animator != null && animationClip != null)
        {
            // Check if animation is not already playing
            if (!isAnimationPlaying && animator.GetCurrentAnimatorStateInfo(0).IsName(animationClip.name))
            {
                // Check if animation has finished playing
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    onAnimationComplete.Invoke();
                    Destroy(gameObject);
                    isAnimationPlaying = false;
                }
            }
        }
    }

    // Play animation if AnimationClip is specified without Animator
    public void PlayAnimation()
    {
        if (animator == null && animationClip != null)
        {
            animator.Play(animationClip.name,0);
            isAnimationPlaying = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayAnimation();
    }

}