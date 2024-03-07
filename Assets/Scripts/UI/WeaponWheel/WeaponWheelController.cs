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

    private Animator animator;
    private WeaponController weaponController;
    public WeaponsEnum Selection { get => selection; set => selection = value; }
    public WeaponController WeaponController { get => weaponController; set => weaponController = value; }
    public WeaponsEnum Hover { get => hover; set => hover = value; }



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void OpenWeaponWheel()
    {
        animator.SetBool("OpenWeaponWheel", true);

    }

    public void CloseWeaponWheel()
    {
        animator.SetBool("OpenWeaponWheel", false);
    }

    public void RotateSelector(float rotationAngle)
    {
         selector.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle ); //* wheelSpeed * Time.deltaTime

  
    }

   

}
