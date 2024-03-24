using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundImage;

    [Header("Inputs: Menu")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private MenuController mainMenuController;

    [Header("Inputs: Weapon UI")]
    [SerializeField]
    private WeaponWheelController weaponWheelController;
    [SerializeField]
    private GameObject weaponWheel;

  
    private WeaponController weaponController;
    private PlayerController playerController;
    private PlayerInputUIProxy playerInputUIProxy;
    private Rigidbody2D playerRigidBody;
    private CameraController followPlayer;
    private GameController gameController;
    private GoalController goalController;
    

    [Header("On Runtime")]
    [SerializeField]
    private MenuState menuState;
    [SerializeField]
    private bool isWeaponWheelOpen = false;
    [SerializeField]
    private bool isMenuOpen = false;
    [SerializeField]
    private bool isPauseMenuOpen = false;
    [SerializeField]
    private bool isControlMenuOpen = false;


    private float weaponWheelRotationAngle = 0;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        weaponController = player.GetComponent<WeaponController>();
        playerController = player.GetComponent<PlayerController>();
        playerInputUIProxy = player.GetComponent<PlayerInputUIProxy>();   
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        followPlayer = Camera.main.gameObject.GetComponent<CameraController>();


        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");

        goalController = gameControllerObj.GetComponent<GoalController>();
        gameController = gameControllerObj.GetComponent<GameController>();
        
    }
    private void Start()
    {
        //Disable Weapon Wheel Buttons By Collected Weapons
        weaponWheelController.UpdateOnGoals(goalController.Weapons);
    }

  

    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (!isMenuOpen)
        {
            MusicController.instance.TurnOff();
          //  Debug.Log("Open Menu");
            isMenuOpen = true;
            mainMenu.SetActive(true);
            mainMenuController.OpenMenu(MenuState.Pause);
            Time.timeScale = 0;
        }
        else
        {
            MusicController.instance.TurnOn();
          //  Debug.Log("Close Menu");
            mainMenuController.CloseMenu();
            isMenuOpen = false;
            mainMenu.SetActive(false);
            playerInputUIProxy.SwitchControlsToPlayer();
            Time.timeScale = 1;
        }
    }

    public void PauseMenu()
    {
        if (!isMenuOpen)
        {
            MusicController.instance.TurnOff();
          //  Debug.Log("Open Menu"); 
            isMenuOpen = true;
            mainMenu.SetActive(true);
            mainMenuController.OpenMenu(MenuState.Pause);
            Time.timeScale = 0;
        }
        else
        {
            MusicController.instance.TurnOn();
         //   Debug.Log("Close Menu");
            mainMenuController.CloseMenu();
            isMenuOpen = false;
            mainMenu.SetActive(false);
            playerInputUIProxy.SwitchControlsToPlayer();
            Time.timeScale = 1;
        }
    }

    public void ControlMenu(InputAction.CallbackContext context)
    {
        if (!isMenuOpen)
        {
         //   Debug.Log("Open Menu");
            isMenuOpen = true;
            mainMenu.SetActive(true);
            mainMenuController.OpenMenu(MenuState.Controls);
            MusicController.instance.TurnOff();
            Time.timeScale = 0;
        }
        else
        {
           // Debug.Log("Close Menu");
            mainMenuController.CloseMenu();
            isMenuOpen = false;
            mainMenu.SetActive(false);
            MusicController.instance.TurnOn();
            Time.timeScale = 1;
            playerInputUIProxy.SwitchControlsToPlayer();
           
        }
    }

    public void ControlMenu()
    {
        if (!isMenuOpen)
        {
          //  Debug.Log("Open Menu");
            isMenuOpen = true;
            mainMenu.SetActive(true);
            mainMenuController.OpenMenu(MenuState.Controls);
            MusicController.instance.TurnOff();
            Time.timeScale = 0;
        }
        else
        {
           // Debug.Log("Close Menu");
            mainMenuController.CloseMenu();
            isMenuOpen = false;
            mainMenu.SetActive(false);
            MusicController.instance.TurnOn();
            Time.timeScale = 1;
            playerInputUIProxy.SwitchControlsToPlayer();

        }
    }


    public void QuitQuestionScreen()
    {
      //  Debug.Log("Quit Question Menu");
        mainMenuController.OpenMenu(MenuState.Quit);
    }


    public void OpenWeaponWheel(InputAction.CallbackContext context)
    {
        if (!isWeaponWheelOpen)
        {
           // playerInputUIProxy.SwitchControlsToWeaponWheel();
            weaponWheel.SetActive(true);
            weaponWheelController.UpdateOnGoals(goalController.Weapons);
            weaponWheelController.OpenWeaponWheel();
            isWeaponWheelOpen = true;
            playerController.enabled = false;
            followPlayer.enabled = false;
            playerRigidBody.simulated = false;
            backgroundImage.SetActive(true);
           // Debug.Log("IsOpen");
        }
    }

    public void CloseWeaponWheel(InputAction.CallbackContext context)
    {
        if (isWeaponWheelOpen)
        {
            playerInputUIProxy.SwitchControlsToPlayer();
            weaponWheelController.CloseWeaponWheel();
            isWeaponWheelOpen = false;
            playerController.enabled = true;
            followPlayer.enabled = true;
            playerRigidBody.simulated = true;
            backgroundImage.SetActive(false);
            weaponController.Select(weaponWheelController.Selection);
            //  Time.timeScale = 1;
            weaponWheel.SetActive(false);

          //  Debug.Log("IsClosed");
        }
    }

    public void MoveWeaponWheel(InputAction.CallbackContext context)
    {
      //  Debug.Log("IsMoving");
        Vector2 input = context.ReadValue<Vector2>();
        float x = input.x;
        float y = input.y;
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f) // Check if right stick is moved significantly
        {
            weaponWheelRotationAngle = Mathf.Atan2(-y, -x) * Mathf.Rad2Deg;
            weaponWheelController.RotateSelector(weaponWheelRotationAngle);
        }
        if (isWeaponWheelOpen)
        {
            
            
        }
    }


    public void QuitGame()
    {
        gameController.QuitGame();
    }

       


   

    

}
