using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerInputUIProxy : MonoBehaviour
{
    private PlayerInput playerInput;

    private UIController uiController;


    private void Awake()
    {
        uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        playerInput = GetComponent<PlayerInput>();

        RegisterUIMethods();
    }

    private void RegisterUIMethods()
    {
        playerInput.actions["OpenWeaponWheel"].started += uiController.OpenWeaponWheel;
      //  playerInput.actions["OpenWeaponWheel"].performed += uiController.OpenWeaponWheel;
        playerInput.actions["OpenWeaponWheel"].canceled += uiController.CloseWeaponWheel;
        playerInput.actions["MoveWeaponWheel"].started += uiController.MoveWeaponWheel;
        playerInput.actions["MoveWeaponWheel"].performed += uiController.MoveWeaponWheel;
        playerInput.actions["OpenPauseMenu"].performed += uiController.PauseMenu;
        playerInput.actions["OpenControlMenu"].performed += uiController.ControlMenu;

    }


    public void SwitchControlsToWeaponWheel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
          /*  if (playerInput.currentActionMap != null)
            {
                Debug.Log(nameof(SwitchControlsToWeaponWheel) + " | Before " + playerInput.currentActionMap.name);
            }*/

            playerInput.SwitchCurrentActionMap("WeaponWheel");

            /*   if (playerInput.currentActionMap != null)
               {
                   Debug.Log(nameof(SwitchControlsToWeaponWheel) + " | After " + playerInput.currentActionMap.name);
               }*/
        }
    }

    public void SwitchControlsToWeaponWheel()
    {

        /* if (playerInput.currentActionMap != null)
         {
             Debug.Log(nameof(SwitchControlsToWeaponWheel) + " | Before " + playerInput.currentActionMap.name);
         }*/

        playerInput.SwitchCurrentActionMap("WeaponWheel");

      /*  if (playerInput.currentActionMap != null)
        {
            Debug.Log(nameof(SwitchControlsToWeaponWheel) + " | After " + playerInput.currentActionMap.name);
         }*/
       
    }

    public void SwitchControlsToPauseUI(InputAction.CallbackContext context)
    {


        if (context.started)
        {
            /* if (playerInput.currentActionMap != null)
             {
                 Debug.Log(nameof(SwitchControlsToPauseUI) + " | Before " + playerInput.currentActionMap.name);
             }*/

            playerInput.SwitchCurrentActionMap("PauseUI");

            /*  if (playerInput.currentActionMap != null)
              {
                  Debug.Log(nameof(SwitchControlsToPauseUI) + " | After " + playerInput.currentActionMap.name);
              }*/
        }
    }

    public void SwitchControlsToPlayer()
    {
        /*  if (playerInput.currentActionMap != null)
          {
              Debug.Log(nameof(SwitchControlsToPlayer) + " | Before " + playerInput.currentActionMap.name);
          }*/

        playerInput.SwitchCurrentActionMap("Player");

        /*   if (playerInput.currentActionMap != null)
           {
               Debug.Log(nameof(SwitchControlsToPlayer) + " | After " + playerInput.currentActionMap.name);
           }*/

    }



}
