using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(WeaponController))]
public class PlayerController : MonoBehaviour
{

   

    [Header("Inputs: Animation")]
    [SerializeField]
    private Animator animator;

    [Header("Inputs: Layers")]
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask deadLayer;
    [SerializeField]
    private LayerMask wallLayer;

    [Header("Inputs: Objects")]
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private TrailRenderer trailRenderer;

    [Header("Inputs: Physics")]
    [SerializeField]
    private float runningSpeed;
    [SerializeField]
    private float jumpingPower;
    [SerializeField]
    private float wallSlidingSpeed;
    [SerializeField]
    private float wallJumpingTime;
    [SerializeField]
    private float wallJumpingDuration;
    [SerializeField]
    private Vector2 wallJumpingPower;
    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private float jumpBufferTime;
    [SerializeField]
    private float dashingPower;
    [SerializeField]
    private float dashingTime;
    [SerializeField]
    private float dashingCooldown;

    [Header("Inputs: SpecialPower")]
    [SerializeField]
    private Transform trompeteRotator;
    [SerializeField]
    private float trompeteShootingPower;
    [SerializeField]
    private float trompeteImpactPower;
    [SerializeField]
    private float trompeteBackForce;
    [SerializeField]
    private float aimingRotationSpeed;



    [Header("Inputs: Animation")]
    [SerializeField]
    private float hittingTimeGuitar;
    [SerializeField]
    private float hittingTimeDrumsticks;
    [SerializeField]
    private float hittingTimePiano;
    [SerializeField]
    private float hittingTimeViolin;
    [SerializeField]
    private float hittingTimeFlute;
    [SerializeField]
    private float hittingTimeTrompete;
    [SerializeField]
    private float specialForceTimeShield;
    

    [Header("What's going on at runtime?")]
    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private bool isFacingRight;
    [SerializeField]
    private bool isWallSliding;
    [SerializeField]
    private bool isWallJumping;
    [SerializeField]
    private bool isHiting;
    [SerializeField]
    private float wallJumpingDirection;
    [SerializeField]
    private float wallJumpingCounter;
    [SerializeField]
    private float coyoteTimeCounter;
    [SerializeField]
    private float jumpBufferCounter;
    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private bool wasJumping;
    [SerializeField]
    private Vector2 dashingDirection;

    [Header("What's going on at runtime? - Special Forces")]
    [SerializeField]
    private bool isProtecting;
    [SerializeField]
    private bool isHealing;
    [SerializeField]
    private bool isHoldingLeftShoulder;
    [SerializeField]
    private bool isAiming;
    [SerializeField]
    private Vector3 weaponRotatorLastEulerAngle;
    [SerializeField]
    private float lastNormalizedAngleRotatorAnimator;


    private VibrationController vibrationController;
    private WeaponController weaponController;

    public Animator Animator { get => animator; set => animator = value; }

    // Start is called before the first frame update
    void Start()
    {
        vibrationController = GameObject.FindGameObjectWithTag("GameController").GetComponent<VibrationController>();
        weaponController = GetComponent<WeaponController>();
    }




    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rigidbody2D.velocity = new Vector2(horizontal * runningSpeed, rigidbody2D.velocity.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        if (isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(!isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (isHealing && horizontal == 0)
        {
            animator.SetBool("isHealing", true);
        }
        else
        {
            animator.SetBool("isHealing", false);
            isHealing = false;
        }


        if (isProtecting && horizontal == 0)
        {
            animator.SetBool("isProtecting", true);
        }
        else
        {
            animator.SetBool("isProtecting", false);
            isProtecting = false;
        }

        WallSlide();
        WallJump();
        OnLanding();

        horizontalSpeed = Input.GetAxisRaw("Horizontal") * runningSpeed;
        SetAnimationState();
    }

    private void OnLanding()
    {
        if (!IsGrounded())
        {
            wasJumping = true;
        }
        else
        {
            if (wasJumping)
            {
              //  vibrationController.SetVibrationByTime(0.075f, 0.125f, 0.15f);
                wasJumping = false;
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer)
            || Physics2D.OverlapCircle(groundCheck.position, 0.5f, deadLayer);
    }


    private bool IsWalking()
    {
        return Mathf.Abs(rigidbody2D.velocity.x) > 0.1f;
    }

    private bool IsWalled() {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }

        if (context.canceled && rigidbody2D.velocity.y > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        } 
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash && !IsGrounded())
        {
            StartCoroutine(ExecuteDash(context));
        }
    }

   private IEnumerator ExecuteDash(InputAction.CallbackContext context)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0f;

        if (Gamepad.current != null)
        {
            dashingDirection = new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            dashingDirection = new Vector2(horizontal, vertical).normalized;
        }

       
        if(dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0);
        }

        rigidbody2D.velocity = dashingDirection.normalized * dashingPower;  //new Vector2(transform.localScale.x * dashingPower, 0f);
        animator.SetBool("isDashing", true);
        trailRenderer.emitting = true;
        
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rigidbody2D.gravityScale = originalGravity;
        animator.SetBool("isDashing", false);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        //animator.SetBool("isfalling", false);
        
        canDash = true;
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Clamp(rigidbody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rigidbody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= 1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    public void Hit()
    {

        if (weaponController.Current != WeaponsEnum.None)
        {
            StartCoroutine(ExecuteHit());
        }
        
    }

    private IEnumerator ExecuteHit()
    {
        Debug.Log("Hit");
        switch (weaponController.Current)
        {
            case WeaponsEnum.Flute:
                animator.SetBool("isHitting", true);
                animator.SetBool("isFlute", true);
                yield return new WaitForSeconds(hittingTimeFlute);
                animator.SetBool("isHitting", false);
                animator.SetBool("isFlute", false);
                break;
            case WeaponsEnum.Trompet:
                animator.SetBool("isHitting", true);
                animator.SetBool("isTrompet", true);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                yield return new WaitForSeconds(hittingTimeTrompete);
                animator.SetBool("isHitting", false);
                animator.SetBool("isTrompet", false);
                break;

            case WeaponsEnum.Violin:
                animator.SetBool("isHitting", true);
                animator.SetBool("isViolin", true);
                yield return new WaitForSeconds(hittingTimeViolin);
                animator.SetBool("isHitting", false);
                animator.SetBool("isViolin", false);
                break;

            case WeaponsEnum.Guitar:
                animator.SetBool("isHitting", true);
                animator.SetBool("isGuitar", true);
                yield return new WaitForSeconds(hittingTimeGuitar);
                animator.SetBool("isHitting", false);
                animator.SetBool("isGuitar", false);
                break;

            case WeaponsEnum.Piano:
                animator.SetBool("isHitting", true);
                animator.SetBool("isPiano", true);
                yield return new WaitForSeconds(hittingTimePiano);
                animator.SetBool("isHitting", false);
                animator.SetBool("isPiano", false);
                break;
            case WeaponsEnum.Drumsticks:
                animator.SetBool("isHitting", true);
                animator.SetBool("isDrumsticks", true);
                yield return new WaitForSeconds(hittingTimeDrumsticks);
                animator.SetBool("isHitting", false);
                animator.SetBool("isDrumsticks", false);
                break;
            case WeaponsEnum.None:

                break;
        }
    }




    public void SpecialForce(InputAction.CallbackContext context)
    {

        if (context.started || context.performed)
        {
            isHoldingLeftShoulder = true;

            switch (weaponController.Current)
            {
                case WeaponsEnum.Flute:
                    if (IsGrounded())
                    {
                        isHealing = true;
                    }
                    else
                    {
                        isHealing = false;
                    }
                    break;
                case WeaponsEnum.Trompet:
                    animator.SetBool("isSpecial", true);
                    animator.SetBool("isTrompet", true);
                    animator.SetBool("isBreathing", true);

                  //  Debug.Log("Is Holding LT");
                    //   animator.SetBool("isShootHolding", true);



                    break;

                case WeaponsEnum.Violin:

                    break;

                case WeaponsEnum.Guitar:

                    break;

                case WeaponsEnum.Piano:

                    break;
                case WeaponsEnum.Drumsticks:

                    if (IsGrounded() && horizontalSpeed == 0)
                    {
                        isProtecting = true;
                        animator.SetBool("isProtecting", true);
                        //   yield return new WaitForSeconds(specialForceTimeShield);
                        animator.SetBool("isProtecting", false);
                    }
                    else
                    {
                        isProtecting = false;
                        animator.SetBool("isProtecting", false);

                    }
                    break;
                case WeaponsEnum.None:

                    break;
            }
        }
        else if (context.canceled)
        {
            isHoldingLeftShoulder = false;

            switch (weaponController.Current)
            {
                case WeaponsEnum.Flute:
                    isHealing = false;
                    break;
                case WeaponsEnum.Trompet:
                    //   animator.SetBool("isShootHolding", false);
                    animator.SetBool("isSpecial", false);
                    animator.SetBool("isTrompet", false);
                    animator.SetBool("isBreathing", false);
                    trompeteRotator.transform.localEulerAngles = Vector3.zero;
                    // rigidbody2D.velocity. = Vector2.zero;


                    break;

                case WeaponsEnum.Violin:

                    break;

                case WeaponsEnum.Guitar:

                    break;

                case WeaponsEnum.Piano:

                    break;
                case WeaponsEnum.Drumsticks:
                    isProtecting = false;
                    animator.SetBool("isProtecting", false);
                    break;
                case WeaponsEnum.None:

                    break;
            }
        }

    }

    /* public IEnumerator ExecuteSpecialForce(InputAction.CallbackContext context)
     {


     }*/


    public void Aim(InputAction.CallbackContext context)
    {
        if (isHoldingLeftShoulder)
        {
            if (context.started || context.performed)
            {
                isAiming = true;

                if (trompeteRotator.gameObject.activeSelf)
                {
                    if (weaponController.Current == WeaponsEnum.Trompet)
                    {
                        //ReadOut
                        float x = context.ReadValue<Vector2>().x;
                        float y = context.ReadValue<Vector2>().y;

                        if (weaponRotatorLastEulerAngle != null)
                        {
                            trompeteRotator.transform.localEulerAngles = weaponRotatorLastEulerAngle;
                        }

                        if (!isFacingRight)
                        {
                            trompeteRotator.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(x, y) * -180 / Mathf.PI + 90f);
                            weaponRotatorLastEulerAngle = trompeteRotator.transform.localEulerAngles;

                            float normalizedAngle = Mathf.InverseLerp(0f, 360f, trompeteRotator.transform.localEulerAngles.z);
                            lastNormalizedAngleRotatorAnimator = normalizedAngle;
                            animator.SetFloat("WeaponAngle", normalizedAngle);
                        }
                        else
                        {
                            trompeteRotator.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(x, y) * 180 / Mathf.PI + 90f);
                            weaponRotatorLastEulerAngle = trompeteRotator.transform.localEulerAngles;

                            float normalizedAngle = Mathf.InverseLerp(0f, 360f, trompeteRotator.transform.localEulerAngles.z);
                            lastNormalizedAngleRotatorAnimator = normalizedAngle;
                            animator.SetFloat("WeaponAngle", normalizedAngle);
                        }


                   /*     Debug.Log("X: " + x + "| Y: " + y + " | Is Rotating Right Stick: " + 0
                              + " |  Rotaton Rotator: " + aimingTest.transform.localEulerAngles +
                              " | Normalized Angle: " + lastNormalizedAngleRotatorAnimator + " | isFacingRight: " + isFacingRight);*/
                    }
                }
            }
            else if (context.canceled)
            {
                isAiming = false;
                // If the right stick input is not active, use the last stored values
                trompeteRotator.transform.localEulerAngles = weaponRotatorLastEulerAngle;
                animator.SetFloat("AngleWeapon", lastNormalizedAngleRotatorAnimator);

            }
        }

            

    }


    public void Shoot(InputAction.CallbackContext context)
    {

        if (isHoldingLeftShoulder && isAiming)
        {
            if (context.started || context.performed)
            {
                Weapon weapon = weaponController.GetCurrent();

                switch (weaponController.Current)
                {
                    case WeaponsEnum.Trompet:
                        weapon.Shoot(trompeteShootingPower, trompeteImpactPower);

                        // Calculate the opposite direction of the aimingTest rotation
                        float rotationAngle = trompeteRotator.transform.eulerAngles.z;
                        float oppositeAngle = trompeteRotator.transform.eulerAngles.z + 180.0f;
                        oppositeAngle = Mathf.DeltaAngle(trompeteRotator.transform.eulerAngles.z, oppositeAngle);


                        if (isFacingRight)
                        {
                          //  Debug.Log("Force - Facing Left");
                            //  rigidbody2D.gravityScale = 0; // No gravity for the bullet

                            rigidbody2D.AddForce(Vector2.left * trompeteBackForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                           // Debug.Log("Force - Facing Right");
                            rigidbody2D.AddForce(Vector2.right * trompeteBackForce, ForceMode2D.Impulse);
                        }
                        break;

                    case WeaponsEnum.Violin:

                        break;

                    case WeaponsEnum.Guitar:

                        break;

                    case WeaponsEnum.Piano:

                        break;
                    case WeaponsEnum.None:

                        break;
                }
                


            }
            else if (context.canceled)
            {


            }

        }
    }

       



    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void SetAnimationState()
    {
        //Idle - doing nothing
        if(horizontalSpeed == 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        //Walking
        else if(horizontalSpeed > 0 || horizontalSpeed < 0 && rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("isWalking", true);
        }

        //Not jumping - not falling
        if(rigidbody2D.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        //Jumping
        else if(rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        //Falling
        else if(rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }




    bool IsUsingGamepad()
    {
        string[] joystickNames = Input.GetJoystickNames();

        // Check if at least one joystick is connected
        return joystickNames.Length > 0;
    }

}
