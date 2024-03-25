using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(EnemySoundController))]
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
    

    [Header("Detection")]
    [SerializeField]
    private float dectionRadius;
    [SerializeField]
    private float attackRadius;

    [Header("Patrol")]
    [SerializeField]
    private Transform patrolPoint;
    [SerializeField]
    private float waitingTimeAtPatrolPoint;
    [SerializeField]
    private float moveSpeedPatrol;
    [SerializeField]
    private float moveSpeedChase;
    [SerializeField]
    private float maxNumbTime;



    [Header("Input: Scripts")]
    [SerializeField]
    private EnemyRangeDetector rangeDetector;
    [SerializeField]
    private EnemyRangeDetector attackRangeDetector;

    [Header("On Runtime")]
    [SerializeField]
    private EnemyState currentState;
    [SerializeField]
    private bool isMovingToTarget = false;
    [SerializeField]
    private bool playerInRange;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float currentWaitingTime;
    [SerializeField]
    private float currentNumbTime;
    [SerializeField]
    private Vector3 startPosition;

    [SerializeField]
    private float life = 100;

    public bool PlayerInRange { get => playerInRange; set => playerInRange = value; }
    public Transform Player { get => player; set => player = value; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(rangeDetector != null && rangeDetector.GetComponent<CircleCollider2D>() != null)
        {
            rangeDetector.GetComponent<CircleCollider2D>().radius = dectionRadius;
        }
        if (attackRangeDetector != null && attackRangeDetector.GetComponent<CircleCollider2D>() != null)
        {
            attackRangeDetector.GetComponent<CircleCollider2D>().radius = attackRadius;
        }
    }
#endif

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }


    void Start()
    {
        // Initialize enemy state
        currentNumbTime = maxNumbTime;
        startPosition = transform.position;
        currentState = EnemyState.Patrol;
    }


    public void Update()
    {


        if (currentState == EnemyState.Numb || currentState == EnemyState.Dead)
        {
            transform.position = transform.position;

            if(currentState == EnemyState.Numb)
            {

                currentNumbTime -= Time.deltaTime;
                if (currentNumbTime <= 0)
                {
                    currentNumbTime = maxNumbTime;
                    ChangeState(EnemyState.Recover);
                    
                    Debug.Log("Herré");
                }
 
            }
        }
        else
        {
            
            if (player != null)
            {
                // Move towards the player
                ChangeState(EnemyState.Chase);
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeedChase * Time.deltaTime);
                isMovingToTarget = false;



            }
            else
            {
                if (patrolPoint != null)
                {
                    if (isMovingToTarget)
                    {
                        ChangeState(EnemyState.Patrol);
                        Vector3 targetPosition = new Vector3(patrolPoint.position.x, transform.position.y, transform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeedPatrol * Time.deltaTime);

                        // Check if reached the target
                        if (Vector3.Distance(transform.position, patrolPoint.position) < 0.1f)
                        {
                            isMovingToTarget = false;
                            currentWaitingTime = waitingTimeAtPatrolPoint;
                        }
                    }
                    else
                    {
                        // Wait for specified time
                        currentWaitingTime -= Time.deltaTime;
                        if (currentWaitingTime <= 0)
                        {
                            // Move back to start position
                            ChangeState(EnemyState.Patrol);

                            Vector3 startTargetPosition = new Vector3(startPosition.x, transform.position.y, transform.position.z);
                            transform.position = Vector3.MoveTowards(transform.position, startTargetPosition, moveSpeedPatrol * Time.deltaTime);




                            // Check if reached start position
                            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                            {
                                isMovingToTarget = true;
                            }
                        }
                        else
                        {
                            ChangeState(EnemyState.Idle);
                        }
                    }
                }
            }
        }

    }

    // Method to change enemy state and invoke corresponding event
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
        
        UpdateAnimation(currentState);

    /*    switch (currentState)
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
            case EnemyState.Defend:
                OnDefend?.Invoke();
                break;
            case EnemyState.SpecialAbility:
                OnSpecialAbility?.Invoke();
                break;
            default:
                break;
        }*/

       // Debug.Log("Current State " + (int)currentState + " " + currentState);
    }

    private void UpdateAnimation(EnemyState state)
    {
        
        animator.SetInteger("State", (int)state);
    }


    public void DestroyMe()
    {
        
        ChangeState(EnemyState.Dead);
    }


    public void HypnotiseMe()
    {
        ChangeState(EnemyState.Numb);
    }

    
}