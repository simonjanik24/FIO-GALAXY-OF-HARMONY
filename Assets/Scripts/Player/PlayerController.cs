using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerSoundController))]
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
    [SerializeField]
    private float slopingAngleThreshold;

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
    [SerializeField]
    private Transform violinSelectorObject;
    [SerializeField]
    private ViolinSelectorObject violinSelectorObjectScript;
    [SerializeField]
    private float violineAimingObjectMoveSpeedNormal = 5f;
    [SerializeField]
    private float violineAimingObjectMoveSpeedPlatform = 0.7f;
    [SerializeField]
    private float violineAimingObjectMaxDistance = 10f;
    [SerializeField]
    private float violineAimingObjectsmoothTime;
    [SerializeField]
    private Transform pianoRotator;
    [SerializeField]
    private float pianoShootingPower;
    [SerializeField]
    private float pianoBackForce;
    [SerializeField]
    private float pianoAimingRotationSpeed;

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
    private float protectingTimeDrumsticks;

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
    private bool isTriggerJumping;
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
    [SerializeField]
    private float currentGroundAngle;

    [Header("What's going on at runtime? - Special Forces")]
    [SerializeField]
    private bool hasShot = false;
    [SerializeField]
    private bool isProtecting;
    [SerializeField]
    private bool isHealing;
    [SerializeField]
    private bool isHoldingLeftShoulder;
    [SerializeField]
    private bool isAiming;
    [SerializeField]
    private bool isHoldingRightShoulder;
    [SerializeField]
    private Vector3 weaponRotatorLastEulerAngle;
    [SerializeField]
    private float lastNormalizedAngleRotatorAnimator;
    [SerializeField]
    private Vector2 violineAimingObjectCurrentVelocity;

    private bool transitionExecuted = false;

    private CameraController cameraController;
    private MusicController musicController;
    private PlayerSoundController soundController;
    private VibrationController vibrationController;
    private WeaponController weaponController;
    private HealthManager healthManager;

    public Animator Animator { get => animator; set => animator = value; }
    public bool IsHoldingRightShoulder { get => isHoldingRightShoulder; set => isHoldingRightShoulder = value; }
    public bool IsHealing { get => isHealing; set => isHealing = value; }
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    public bool IsHiting { get => isHiting; set => isHiting = value; }
    public float Horizontal { get => horizontal; set => horizontal = value; }

  

    // Start is called before the first frame update
    void Start()
    {
        // vibrationController = GameObject.FindGameObjectWithTag("GameController").GetComponent<VibrationController>();
        soundController = GetComponent<PlayerSoundController>();
        weaponController = GetComponent<WeaponController>();
        healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
        cameraController = Camera.main.gameObject.GetComponent<CameraController>(); 
 
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

        SaveCurrentSlope();


        if (currentGroundAngle > slopingAngleThreshold)
        {
          //  rigidbody2D.velocity = new Vector2(horizontal * (runningSpeed *3), rigidbody2D.velocity.y);
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
            healthManager.Heal();
        }
        else
        {
            //isHealing = false;
            animator.SetBool("isHealing", false);      
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

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isTriggerJumping)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        WallSlide();
        WallJump();
        OnLanding();

        horizontalSpeed = Input.GetAxisRaw("Horizontal") * runningSpeed;
       SetAnimationState();


      //  DrawLine();
    }



    private void DrawLine()
    {
        // Instantiate a cube at the current position with a scale of (0.1, 0.1, 0.1) and white color
        GameObject newShape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newShape.transform.position = transform.position;
        newShape.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Assign white material to the shape
        Renderer shapeRenderer = newShape.GetComponent<Renderer>();
        if (shapeRenderer != null)
        {
            shapeRenderer.material.color = Color.red;
        }
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
                soundController.PlayOnLandingSound();
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }


    public void SaveCurrentSlope()
    {
        if (IsGrounded())
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 5f, groundLayer);

            if (hit.collider != null)
            {
                currentGroundAngle = Vector2.Angle(hit.normal, Vector2.up);
            }
        }
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

        if (IsGrounded() && horizontal != 0) {
            soundController.PlayWalkSound();
        }
        else
        {
            soundController.StopWalkSound();
        }
  
        if(isHealing && horizontal != 0)
        {
          MusicController.instance.TransitionToMainMusic();
          Debug.Log("Transition Music");
          cameraController.Follow();
          isHealing = false;

        }
   
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            isTriggerJumping = true;
            jumpBufferCounter = jumpBufferTime;
           /* if (IsGrounded() && !wasJumping || IsWalled() || isWallSliding ||isWallJumping)
            {
                Debug.Log("Sound On Jump");
                
            }*/
            soundController.PlayJumpSound();

        }
        else if (context.canceled)
        {
            isTriggerJumping = false;
            jumpBufferCounter -= Time.deltaTime;
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
        soundController.PlayDashSound();
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
            Debug.Log("IsWallSlide");
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
        
        if (!IsGrounded() && isTriggerJumping && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            Flip();
            rigidbody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;         
            
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
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
        soundController.PlaySwingSound();
        switch (weaponController.Current)
        {
            case WeaponsEnum.Flute:
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isFlute", true);
                yield return new WaitForSeconds(hittingTimeFlute);
                animator.SetBool("isHitting", false);
                animator.SetBool("isFlute", false);
                isHiting = false;
                break;
            case WeaponsEnum.Trompet:
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isTrompet", true);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                yield return new WaitForSeconds(hittingTimeTrompete);
                animator.SetBool("isHitting", false);
                animator.SetBool("isTrompet", false);
                isHiting = false;
                break;

            case WeaponsEnum.Violin:
                Violin violin = (Violin) weaponController.GetCurrent();
                violin.StopAll();
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isViolin", true);
                yield return new WaitForSeconds(hittingTimeViolin);
                animator.SetBool("isHitting", false);
                animator.SetBool("isViolin", false);
                isHiting = false;
                break;

            case WeaponsEnum.Guitar:
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isGuitar", true);
                yield return new WaitForSeconds(hittingTimeGuitar);
                animator.SetBool("isHitting", false);
                animator.SetBool("isGuitar", false);
                isHiting = false;
                break;

            case WeaponsEnum.Piano:
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isPiano", true);
                yield return new WaitForSeconds(hittingTimePiano);
                animator.SetBool("isHitting", false);
                animator.SetBool("isPiano", false);
                isHiting = false;
                break;
            case WeaponsEnum.Drumsticks:
                isHiting = true;
                animator.SetBool("isHitting", true);
                animator.SetBool("isDrumsticks", true);
                yield return new WaitForSeconds(hittingTimeDrumsticks);
                animator.SetBool("isHitting", false);
                animator.SetBool("isDrumsticks", false);
                isHiting = false;
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

                    if (IsGrounded() || !IsWalking())
                    {
                        Debug.Log("Healing");
                        Flute flute = (Flute)weaponController.GetCurrent();
                        if (isFacingRight)
                        {
                            flute.FlipRight();
                        }
                        else
                        {
                            flute.FlipLeft();
                        }
                        isHealing = true;
                        MusicController.instance.TransitionToHealingMusic();
                        cameraController.Zoom();

                    }
                    else
                    {
                        isHealing = false;
                        Debug.Log("Is Not Healing");
                        MusicController.instance.TransitionToMainMusic();
                        cameraController.Follow();
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
                    animator.SetBool("isSpecial", true);
                    animator.SetBool("isViolin", true);

                    Violin violin = (Violin)weaponController.GetCurrent();
                    violin.StartHovering();
                    

                    break;

                case WeaponsEnum.Guitar:
                    animator.SetBool("isSpecial", true);
                    animator.SetBool("isGuitar", true);
                    break;

                case WeaponsEnum.Piano:
                    animator.SetBool("isSpecial", true);
                    animator.SetBool("isPiano", true);



                    break;

                case WeaponsEnum.Drumsticks:

                    if (IsGrounded() && horizontalSpeed == 0)
                    {
                        isProtecting = true;
                        animator.SetBool("isProtecting", true);
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
                    MusicController.instance.TransitionToMainMusic();
                    cameraController.Follow();
                    Debug.Log("Healing Canceled");
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
                    animator.SetBool("isSpecial",false);
                    animator.SetBool("isViolin", false);
                    animator.SetBool("isViolinPlaying", false);
                    Violin violin = (Violin)weaponController.GetCurrent();
                    violin.StopAll();

                    break;

                case WeaponsEnum.Guitar:
                    animator.SetBool("isSpecial", false);
                    animator.SetBool("isGuitar", false);
                    break;

                case WeaponsEnum.Piano:
                    animator.SetBool("isSpecial", true);
                    animator.SetBool("isPiano", true);
                    pianoRotator.transform.localEulerAngles = Vector3.zero;

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

    public void Aim(InputAction.CallbackContext context)
    {

        if (isHoldingLeftShoulder)
        {
            if (weaponController.Current == WeaponsEnum.Trompet)
            {
                if (context.started || context.performed)
                {
                    isAiming = true;

                    if (trompeteRotator.gameObject.activeSelf)
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


                    if (pianoRotator.gameObject.activeSelf)
                    {
                        //ReadOut
                        float x = context.ReadValue<Vector2>().x;
                        float y = context.ReadValue<Vector2>().y;

                        if (weaponRotatorLastEulerAngle != null)
                        {
                            pianoRotator.transform.localEulerAngles = weaponRotatorLastEulerAngle;
                        }

                        if (!isFacingRight)
                        {
                            pianoRotator.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(x, y) * -180 / Mathf.PI + 90f);
                            weaponRotatorLastEulerAngle = pianoRotator.transform.localEulerAngles;

                            float normalizedAngle = Mathf.InverseLerp(0f, 360f, pianoRotator.transform.localEulerAngles.z);
                            lastNormalizedAngleRotatorAnimator = normalizedAngle;
                            animator.SetFloat("WeaponAngle", normalizedAngle);
                        }
                        else
                        {
                            pianoRotator.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(x, y) * 180 / Mathf.PI + 90f);
                            weaponRotatorLastEulerAngle = pianoRotator.transform.localEulerAngles;

                            float normalizedAngle = Mathf.InverseLerp(0f, 360f, pianoRotator.transform.localEulerAngles.z);
                            lastNormalizedAngleRotatorAnimator = normalizedAngle;
                            animator.SetFloat("WeaponAngle", normalizedAngle);
                        }
                    }
                }
                else if (context.canceled)
                {
                    isAiming = false;
                    if (trompeteRotator.gameObject.activeSelf)
                    {
                        trompeteRotator.transform.localEulerAngles = weaponRotatorLastEulerAngle;
                        animator.SetFloat("AngleWeapon", lastNormalizedAngleRotatorAnimator);
                    }
                    else if (pianoRotator.gameObject.activeSelf)
                    {
                        pianoRotator.transform.localEulerAngles = weaponRotatorLastEulerAngle;
                        animator.SetFloat("AngleWeapon", lastNormalizedAngleRotatorAnimator);
                    }
                 
                }

            }

            if (weaponController.Current == WeaponsEnum.Violin)
            {
                Vector2 input = context.ReadValue<Vector2>();
                if (input.magnitude != 0)
                {
                    isAiming = true;
                    Vector2 newPosition = violinSelectorObject.transform.position + new Vector3(input.x, input.y);
                    violinSelectorObjectScript.Move(newPosition);
                }
                else
                {
                    isAiming = false;
                }


            }


        }
        else
        {
            violinSelectorObjectScript.Reset();
            animator.SetBool("isVolinPlaying", false);
            animator.SetBool("isSpecial", false);
            animator.SetBool("isViolin", false);
            isAiming = false;
   
        
        }

       
    }


    public void Shoot(InputAction.CallbackContext context)
    {

        if (isHoldingLeftShoulder && isAiming)
        {
            if (context.started && !hasShot)
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
                            rigidbody2D.AddForce(Vector2.left * trompeteBackForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                            rigidbody2D.AddForce(Vector2.right * trompeteBackForce, ForceMode2D.Impulse);
                        }
                        soundController.PlayShootSound();
                        hasShot = true;

                        break;

                    case WeaponsEnum.Violin:


                        break;

                    case WeaponsEnum.Guitar:

                        break;

                    case WeaponsEnum.Piano:
                        weapon.Shoot(trompeteShootingPower, trompeteImpactPower);
                        // Calculate the opposite direction of the aimingTest rotation
                        float rotationAngle1 = trompeteRotator.transform.eulerAngles.z;
                        float oppositeAngle1 = trompeteRotator.transform.eulerAngles.z + 180.0f;
                        oppositeAngle1 = Mathf.DeltaAngle(pianoRotator.transform.eulerAngles.z, oppositeAngle1);

                        if (isFacingRight)
                        {
                            rigidbody2D.AddForce(Vector2.left * pianoBackForce, ForceMode2D.Impulse);
                        }
                        else
                        {
                            rigidbody2D.AddForce(Vector2.right * pianoBackForce, ForceMode2D.Impulse);
                        }

                        hasShot = true;


                        break;
                    case WeaponsEnum.None:

                        break;
                }
                


            }
            else if (context.canceled)
            {
                hasShot = false;
            }

        }

        
        if (weaponController.Current == WeaponsEnum.Violin) {

            if (context.started || context.performed)
            {
                Violin violin = (Violin)weaponController.GetCurrent();
                isHoldingRightShoulder = true;
                animator.SetBool("isVolinPlaying", true);
                violin.StartMovingObject();
                MusicController.instance.TransiationToViolinMusic();
            }
            else if (context.canceled)
            {
                MusicController.instance.TransitionToMainMusic();
                isHoldingRightShoulder = false;
                animator.SetBool("isVolinPlaying", false);
            }

        
        }
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
          //  MusicController.instance.TransitionToMainMusic();
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
            if (IsGrounded())
            {
                animator.SetBool("isJumping", false);
            }
            else
            {
                animator.SetBool("isJumping", true);
            }

            
        }
        //Falling
        else if(rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            if (IsGrounded())
            {
                animator.SetBool("isFalling", false);
            }
            else
            {
                animator.SetBool("isFalling", true);
            } 
        }


        if(currentGroundAngle > slopingAngleThreshold)
        {
            if (IsGrounded())
            {
                animator.SetBool("isSlipping", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isSlipping", false);
            }
            
            
        }
        else
        {
            animator.SetBool("isSlipping", false);
        }
    }




    bool IsUsingGamepad()
    {
        string[] joystickNames = Input.GetJoystickNames();

        // Check if at least one joystick is connected
        return joystickNames.Length > 0;
    }

}
