using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponWheelController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private GameObject selector;
    [SerializeField]
    private float wheelSpeed;

    [Header("On Runtime")]
    [SerializeField]
    private WeaponsEnum selection = WeaponsEnum.None;
    [SerializeField]
    private WeaponsEnum hover = WeaponsEnum.None;
    [SerializeField]
    private bool isOpen = false;

    private Animator animator;
    private WeaponController weaponController;

    public WeaponsEnum Selection { get => selection; set => selection = value; }
    public WeaponController WeaponController { get => weaponController; set => weaponController = value; }
    public WeaponsEnum Hover { get => hover; set => hover = value; }
    public bool IsOpen { get => isOpen; set => isOpen = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void OpenWeaponWheel()
    {
        isOpen = true;
        animator.SetBool("OpenWeaponWheel", true);

    }

    public void CloseWeaponWheel()
    {
        isOpen = false;
        animator.SetBool("OpenWeaponWheel", false);
    }

    public void RotateSelector(float rotationAngle)
    {
         selector.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle ); //* wheelSpeed * Time.deltaTime
    }

    public void UpdateOnGoals(List<WeaponsEnum> weapons)
    {
        if(weapons != null)
        {
            if (weapons.Count != 0)
            {
                Debug.Log("Count is on");
                // Enable buttons for each weapon
                foreach (Transform child in transform)
                {
                    if (child.tag == "WeaponWheelButton")
                    {
                        WeaponWheelButton button = child.gameObject.GetComponent<WeaponWheelButton>();
                        bool foundMatch = false; // Flag to track if a matching weapon button is found
                        foreach (WeaponsEnum weapon in weapons)
                        {
                            if (weapon == button.Weapon)
                            {
                                button.Enable();
                                foundMatch = true;
                                break; // Once a matching button is found, no need to continue searching
                            }
                        }
                        if (!foundMatch)
                        {
                            button.Disable();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Count is 0");
                foreach (Transform child in transform)
                {
                    if (child.tag == "WeaponWheelButton")
                    {
                        WeaponWheelButton button = child.gameObject.GetComponent<WeaponWheelButton>();
                        button.Disable();
                    }
                }
            }
        }
        else
        {
            Debug.Log("Count is null");
            foreach (Transform child in transform)
            {
                if (child.tag == "WeaponWheelButton")
                {
                    WeaponWheelButton button = child.gameObject.GetComponent<WeaponWheelButton>();
                    button.Disable();
                }
            }
        }
        
    }
}

