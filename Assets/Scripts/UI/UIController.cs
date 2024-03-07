using UnityEngine;
using UnityEngine.InputSystem;


public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundImage;

    [Header("Inputs: Weapon UI")]
    [SerializeField]
    private WeaponWheelController weaponWheelController;
    [SerializeField]
    private GameObject weaponWheel;

    private WeaponController weaponController;
    private PlayerController playerController;
    private Rigidbody2D playerRigidBody;
    private CameraController followPlayer;

    private bool isWeaponWheelOpen = false;

    private float weaponSelectorAngleX;
    private float weaponSelectorAngleY;
    private bool isMovingRightStick = false;

    private float weaponWheelRotationAngle = 0;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        weaponController = player.GetComponent<WeaponController>();
        playerController = player.GetComponent<PlayerController>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        followPlayer = Camera.main.gameObject.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            WeaponWheel();
        }

    }

    private void WeaponWheel()
    {
        if (PlayerControls.Instance.OpenWeaponWheel.IsPressed())
        {
           

            if (!isWeaponWheelOpen)
            {
                weaponWheel.SetActive(true);

                weaponWheelController.OpenWeaponWheel();
                isWeaponWheelOpen = true;
                playerController.enabled = false;
                followPlayer.enabled = false;
                playerRigidBody.simulated = false;
                backgroundImage.SetActive(true);

            }
            else
            {
                float x = Gamepad.current.rightStick.ReadValue().x;
                float y = Gamepad.current.rightStick.ReadValue().y;
                if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f) // Check if right stick is moved significantly
                {
                    weaponWheelRotationAngle = Mathf.Atan2(-y, -x) * Mathf.Rad2Deg;
                    weaponWheelController.RotateSelector(weaponWheelRotationAngle);
                }

            }
        }
        else
        {
            if (isWeaponWheelOpen)
            {
                
                weaponWheelController.CloseWeaponWheel();
                isWeaponWheelOpen = false;
                playerController.enabled = true;
                followPlayer.enabled = true;
                playerRigidBody.simulated = true;
                backgroundImage.SetActive(false);
                weaponController.Select(weaponWheelController.Selection);
                //  Time.timeScale = 1;
                weaponWheel.SetActive(false); 

            }
        }

     }
}
