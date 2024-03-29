using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class WeaponController : MonoBehaviour
{

    [Header("Inputs: Weapons")]
    [SerializeField]
    private Transform weapons;

    [Header("Inputs: Shield")]
    [SerializeField]
    private Shield shield;

    [Header("What's going on at runtime?")]
    [SerializeField]
    private WeaponsEnum current;

    private PlayerController playerController;

    public WeaponsEnum Current { get => current; set => current = value; }
    public Shield Shield { get => shield; set => shield = value; }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        Select(WeaponsEnum.None);
      //  current = WeaponsEnum.None;
    }


    public void Select(WeaponsEnum selection)
    {
        current = selection;
        switch (selection)
        {
            case WeaponsEnum.Flute:
                SetVisible(current);
                SetParameterBy(current, 1, playerController.Animator);

                break;
            case WeaponsEnum.Trompet:
                SetVisible(current);
                SetParameterBy(current, 4, playerController.Animator);
                break;

            case WeaponsEnum.Violin:
                SetVisible(current);
                SetParameterBy(current, 5, playerController.Animator);
                break;

            case WeaponsEnum.Guitar:
                SetVisible(current);
                SetParameterBy(current, 2, playerController.Animator);
                break;

            case WeaponsEnum.Piano:
                SetVisible(current);
                SetParameterBy(current, 3, playerController.Animator);
                break;
            case WeaponsEnum.Drumsticks:
                SetVisible(current);
                SetParameterBy(current, 0, playerController.Animator);
                break;
            case WeaponsEnum.None:
                SetVisible(current);
                SetParameterBy(current, 6, playerController.Animator);
                break;
        }
    }

    private void SetVisible(WeaponsEnum selection)
    {
        foreach (Transform _weapon in weapons)
        {
            if(_weapon.gameObject.GetComponent<Weapon>().Typ == selection)
            {
                _weapon.gameObject.SetActive(true);
              //  Debug.Log(_weapon.gameObject.name);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }
        }
    }


    private void SetParameterBy(WeaponsEnum selection, float threshold, Animator animator)
    {
        List<string> parametersList = new List<string>();

        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if(parameter.name.Contains(WeaponsEnum.Trompet.ToString()) ||
               parameter.name.Contains(WeaponsEnum.Piano.ToString()) ||
               parameter.name.Contains(WeaponsEnum.Violin.ToString()) ||
               parameter.name.Contains(WeaponsEnum.Flute.ToString()) ||
               parameter.name.Contains(WeaponsEnum.Drumsticks.ToString()) ||
               parameter.name.Contains(WeaponsEnum.Guitar.ToString()))
            {
                parametersList.Add(parameter.name);
            }
        }

        foreach(string parameter in parametersList)
        {
            if (parameter.Contains(selection.ToString()))
            {
                animator.SetBool(parameter, true);
              //  Debug.Log("Set Parameter to true: " + parameter);
            }
            else
            {
                animator.SetBool(parameter, false);
            }
            
        }
        animator.SetFloat("Weapon", threshold);
    }


    public Weapon GetCurrent()
    {
        Weapon weapon = null;

        foreach (Transform _weapon in weapons)
        {
            if (_weapon.gameObject.GetComponent<Weapon>().Typ == current)
            {
                if(_weapon.gameObject.GetComponent<Weapon>().Typ != WeaponsEnum.None)
                {
                    weapon = _weapon.gameObject.GetComponent<Weapon>();
                    break;
                }
            }
        }
        return weapon;
    }


}
