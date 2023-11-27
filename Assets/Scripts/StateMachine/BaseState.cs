
using TMPro.EditorUtilities;
using UnityEngine;

public class BaseState : IState
{
    protected readonly PlayerController player;
    protected readonly Animator animator;

    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");

    protected const float crossFadeDuration = 0.1f;

    protected BaseState(PlayerController player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

     public void FixedUpdate()
    {
      
    }

    public void OnEnter()
    {
       
    }

    public void OnExit()
    {
       
    }

    public void Update()
    {

    }
}
