using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Notification : MonoBehaviour
{

    [SerializeField]
    private ComputingPlatform computingPlatform;

    [SerializeField]
    private UnityEvent unityEventBeforeDeactivation;
    [SerializeField]
    private List<XboxNotificationCondition> requieredActionsToDeactivation;
    // Start is called before the first frame update

    private WeaponController weaponController;

    private int counter = 0;

    private void OnValidate()
    {
        computingPlatform = transform.parent.GetComponent<NotificationController>().ComputingPlatform;
    }

    private void Awake()
    {
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>();
    }
    void Start()
    {
        if (computingPlatform == ComputingPlatform.PC)
        {

        }
        else if (computingPlatform == ComputingPlatform.Xbox)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(requieredActionsToDeactivation != null &&
            requieredActionsToDeactivation.Count > 0)
        {
            foreach (XboxNotificationCondition input in requieredActionsToDeactivation)
            {
                MapXboxGamePadInputToAction(input);
                if (input != null)
                {
                    if (input.inputCondition && 
                        input.weaponCondition)
                    {
                        counter++;

                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }

            if (counter == requieredActionsToDeactivation.Count)
            {
                DestroyMe();
            }
        }
       
    }


    public void DestroyMe()
    {
        if(unityEventBeforeDeactivation != null)
        {
            unityEventBeforeDeactivation.Invoke();
        }
        Destroy(gameObject);
    }

    private void MapXboxGamePadInputToAction(XboxNotificationCondition input)
    {
        {
            if(input.weapon == WeaponsEnum.None)
            {
                input.weaponCondition = true;
            }
            else
            {
                if (input.weapon == weaponController.Current)
                {
                    input.weaponCondition = true;
                }
                else
                {
                    input.weaponCondition = false;

                }
            }
            

            switch (input.input)
            {
                case XboxGamePadInputs.None:
                    input.inputCondition = true;
                    break;

                case XboxGamePadInputs.A:
                    if (Gamepad.current.aButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.B:
                    if (Gamepad.current.bButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.X:
                    if (Gamepad.current.xButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.Y:
                    if (Gamepad.current.yButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftBumper:
                    if (Gamepad.current.leftShoulder.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightBumper:
                    if (Gamepad.current.rightShoulder.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftTrigger:
                    if (Gamepad.current.leftTrigger.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightTrigger:
                    if (Gamepad.current.rightTrigger.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                /*  case XboxGamePadInputs.Share:
                      if (Gamepad.current.?.isPressed)
                      {
                          input.condition = true;
                      }
                      break;*/
                /* case XboxGamePadInputs.View:
                     if (Gamepad.current.?.isPressed)
                     {
                         input.condition = true;
                     }

                     break; 
                case XboxGamePadInputs.Menu:
                    Debug.Log("Menu button pressed");
                    break;*/
                case XboxGamePadInputs.DPadUp:
                    if (Gamepad.current.dpad.up.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.DPadDown:
                    if (Gamepad.current.dpad.down.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.DPadLeft:
                    if (Gamepad.current.dpad.left.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.DPadRight:
                    if (Gamepad.current.dpad.right.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftStick:
                    if (Gamepad.current.leftStick.value.x != 0 ||
                        Gamepad.current.leftStick.value.y != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightStick:
                    if (Gamepad.current.rightStick.value.x != 0 ||
                        Gamepad.current.rightStick.value.y != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftStickX:
                    if (Gamepad.current.leftStick.value.x != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftStickY:
                    if (Gamepad.current.leftStick.value.y != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightStickX:
                    if (Gamepad.current.rightStick.value.x != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightStickY:
                    if (Gamepad.current.rightStick.value.y != 0)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.LeftStickButton:
                    if (Gamepad.current.leftStickButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
                case XboxGamePadInputs.RightStickButton:
                    if (Gamepad.current.rightStickButton.isPressed)
                    {
                        input.inputCondition = true;
                    }
                    break;
            }
        }
    }

}

[Serializable]
public class XboxNotificationCondition
{
    public bool inputCondition;
    public bool weaponCondition;
    public XboxGamePadInputs input;
    public WeaponsEnum weapon = WeaponsEnum.None;

    public override string ToString()
    {
        return "Condition: " + input + " | " + inputCondition;
    }


}
