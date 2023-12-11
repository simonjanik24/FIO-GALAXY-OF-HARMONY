using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private bool isSelected = false;

    private Animator animator;
    private Button button;
    private Image image;
    

    // Start is called before the first frame update
    void Start()
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
        isSelected = true;
        weaponWheelController.Selection = weapon;
        image.color = button.colors.highlightedColor;
    }

    public void Deselected()
    {
        isSelected = false;
        weaponWheelController.Selection = WeaponsEnum.None;
        image.color = button.colors.normalColor;
    }
  
    public void HoverEnter()
    {
        isSelected = true;
        animator.SetBool("isHovering", true);
        weaponWheelController.Selection = weapon;
        image.color = button.colors.highlightedColor;
        

      //  itemText.text = weapon.ToString();
    }

    public void HoverExit()
    {
        isSelected = false;
        animator.SetBool("isHovering", false);
        weaponWheelController.Selection = WeaponsEnum.None;
        image.color = button.colors.normalColor;

        //  itemText.text = "";
    }

    public void HoverStay()
    {
        isSelected = true;
        weaponWheelController.Selection = weapon;
        image.color = button.colors.highlightedColor;
    }

}
