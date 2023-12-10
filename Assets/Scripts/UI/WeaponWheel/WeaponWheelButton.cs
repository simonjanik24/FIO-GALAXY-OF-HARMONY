using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponWheelButton : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private WeaponsEnum weapon;
    [SerializeField]
    private WeaponWheelController weaponWheelController;
    [SerializeField]
    private TextMeshProUGUI itemText;
    [SerializeField]
    private Sprite icon;

    [Header("What's going on at runtime?")]
    [SerializeField]
    private bool isSelected = false;

    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icon;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isSelected)
        {
            itemText.text = weapon.ToString();
        }

    }

    public void Selected()
    {
        isSelected = true;
        weaponWheelController.Selection = weapon;
    }

    public void Deselected()
    {
        isSelected = false;
        weaponWheelController.Selection = WeaponsEnum.None;
    }
  
    public void HoverEnter()
    {
        animator.SetBool("isHovering", true);
        weaponWheelController.Hover = weapon;

      //  itemText.text = weapon.ToString();
    }

    public void HoverExit()
    {
        animator.SetBool("isHovering", false);
        weaponWheelController.Hover = WeaponsEnum.None;

        //  itemText.text = "";
    }

}
