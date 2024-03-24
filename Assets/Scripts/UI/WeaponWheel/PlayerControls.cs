// PlayerControls.cs
// This class represents your input actions. You need to generate it using the Input System.

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance; // Singleton pattern

    public InputAction OpenWeaponWheel;

    //public InputAction OpenMenu; 


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize your input actions
        OpenWeaponWheel = new InputAction(binding: "<Gamepad>/leftShoulder");

     //   OpenMenu = new InputAction(binding: "<Gamepad>/start");
    }

    private void OnEnable()
    {
        // Enable the input actions
        OpenWeaponWheel.Enable();
      //  OpenMenu.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        OpenWeaponWheel.Disable();
       // OpenMenu.Disable();
    }
}