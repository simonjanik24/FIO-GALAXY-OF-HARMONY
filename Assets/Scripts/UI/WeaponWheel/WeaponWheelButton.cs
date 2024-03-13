using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
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
    private bool isEnabled = false;
    [SerializeField]
    private bool isSelected = false;

    private Animator animator;
    private Button button;
    private Image image;
    

    public WeaponsEnum Weapon { get => weapon; set => weapon = value; }


    private void Awake()
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icon;
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (isSelected)
        {
            itemText.text = weaponWheelController.Selection.ToString();
        }

    }

    public void Selected()
    {
        if (isEnabled)
        {
            isSelected = true;
            weaponWheelController.Selection = weapon;
            image.color = button.colors.highlightedColor;
        }
        else
        {
            weaponWheelController.Selection = WeaponsEnum.None;
        }
       
    }

    public void Deselected()
    {
        if (isEnabled)
        {
            isSelected = false;
            weaponWheelController.Selection = WeaponsEnum.None;
            image.color = button.colors.normalColor;
        }
        else
        {
            weaponWheelController.Selection = WeaponsEnum.None;
        }

    }
  
    public void HoverEnter()
    {
        if (isEnabled)
        {
            isSelected = true;
            animator.SetBool("isHovering", true);
            weaponWheelController.Selection = weapon;
            image.color = button.colors.highlightedColor;
        }
        else
        {
            weaponWheelController.Selection = WeaponsEnum.None;
        }

        //  itemText.text = weapon.ToString();
    }

    public void HoverExit()
    {
        if (isEnabled)
        {
            isSelected = false;
            animator.SetBool("isHovering", false);
            weaponWheelController.Selection = WeaponsEnum.None;
            image.color = button.colors.normalColor;
        }
        else
        {
            weaponWheelController.Selection = WeaponsEnum.None;
        }

        //  itemText.text = "";
    }

    public void HoverStay()
    {
        if (isEnabled)
        {
            isSelected = true;
            weaponWheelController.Selection = weapon;
            image.color = button.colors.highlightedColor;
        }
        else
        {
            weaponWheelController.Selection = WeaponsEnum.None;
        }
        
    }


    public void Enable()
    {
        isEnabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void Disable()
    {
        isEnabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Button>().interactable = false;
    }
}
