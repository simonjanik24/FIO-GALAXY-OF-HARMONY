using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    private Vector2 dashingDirection;



    public Animator Animator { get => animator; set => animator = value; }

    // Start is called before the first frame update
    void Start()
    {
        
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

        WallSlide();
        WallJump();

        horizontalSpeed = Input.GetAxisRaw("Horizontal") * runningSpeed;
        SetAnimationState(); 
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)
            || Physics2D.OverlapCircle(groundCheck.position, 0.2f, deadLayer);
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
        if (canDash)
        {
            StartCoroutine(ExecuteDash(context));
        }
    }

   private IEnumerator ExecuteDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0f;

        if (IsUsingGamepad())
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
