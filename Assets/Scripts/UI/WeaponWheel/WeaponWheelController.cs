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

    [Header("What's going on at runtime?")]
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
            if(weapons.Count != 0)
            {
                foreach (WeaponsEnum weapon in weapons)
                {
                    foreach (Transform child in transform)
                    {
                        if (child.tag == "WeaponWheelButton")
                        {
                            WeaponWheelButton button = child.gameObject.GetComponent<WeaponWheelButton>();

                            if (weapon == button.Weapon)
                            {
                                button.Enable();
                            }
                            else
                            {
                                button.Disable();
                            }

                        }
                    }
                }
            }
            else
            {
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

