using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundImage;

    [Header("Inputs: Weapon UI")]
    [SerializeField]
    private WeaponWheelController weaponWheelController;

    private WeaponController weaponController;
    private PlayerController playerController;
    private FollowPlayer followPlayer;

    private bool isWeaponWheelOpen = false;

    private float weaponSelectorAngleX;
    private float weaponSelectorAngleY;
    private bool isMovingRightStick = false;

    private void Start()
    {
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        followPlayer = Camera.main.gameObject.GetComponent<FollowPlayer>();
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

                weaponWheelController.OpenWeaponWheel();
                isWeaponWheelOpen = true;
                playerController.enabled = false;
                followPlayer.enabled = false;
                backgroundImage.SetActive(true);
                Debug.Log("Weapon Wheel opened");
                Time.timeScale = 0;
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
                backgroundImage.SetActive(false);
                Debug.Log("Weapon Wheel closed");
                Time.timeScale = 1;

            }
        }

        if (isWeaponWheelOpen)
        {
            float x = Gamepad.current.rightStick.ReadValue().x;
            float y = Gamepad.current.rightStick.ReadValue().y;
            float rotationAngle = Mathf.Atan2(-y, -x) * Mathf.Rad2Deg;
            Quaternion angleUnity = Quaternion.Euler(0f, 0f, rotationAngle);
            weaponWheelController.RotateSelector(rotationAngle);
        }

     }
}
