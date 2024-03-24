using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    // Define event handlers for different enemy states
    public event Action OnIdle;
    public event Action OnPatrol;
    public event Action OnAlert;
    public event Action OnChase;
    public event Action OnAttack;
    public event Action OnHit;
    public event Action OnDefend;
    public event Action OnRecovery;
    public event Action OnFlee;
    public event Action OnSpecialAbility;
    public event Action OnSearch;

    private Animator animator;
    // Current state of the enemy
    private EnemyState currentState;

    [Header("Inputs")]
    [SerializeField]
    private float dectionRadius;

    [Header("Scripts")]
    [SerializeField]
    private EnemyRangeDetector rangeDetector;
    
    [Header("On Runtime")]
    [SerializeField]
    private bool playerInRange;
    [SerializeField]
    private float life = 100;

    public bool PlayerInRange { get => playerInRange; set => playerInRange = value; }


    private void OnValidate()
    {
        rangeDetector.gameObject.GetComponent<CircleCollider2D>().radius = dectionRadius;


        
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // Initialize enemy state
        currentState = EnemyState.Idle;
    }

    // Method to change enemy state and invoke corresponding event
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;

        UpdateAnimation(newState);

        switch (newState)
        {
            case EnemyState.Idle:
                OnIdle?.Invoke();
                break;
            case EnemyState.Patrol:
                OnPatrol?.Invoke();
                break;
            case EnemyState.Alert:
                OnAlert?.Invoke();
                break;
            case EnemyState.Chase:
                OnChase?.Invoke();
                break;
            case EnemyState.Attack:
                OnAttack?.Invoke();
                break;
            case EnemyState.Hit:
                OnHit?.Invoke();
                break;
            case EnemyState.Defend:
                OnDefend?.Invoke();
                break;
            case EnemyState.SpecialAbility:
                OnSpecialAbility?.Invoke();
                break;
            default:
                break;
        }
    }

    // Method to update animation based on the enemy state
    private void UpdateAnimation(EnemyState state)
    {
        // Assuming animator has parameters for different states
        // You may need to adjust this based on your animator setup
        animator.SetInteger("State", (int)state);
    }

}