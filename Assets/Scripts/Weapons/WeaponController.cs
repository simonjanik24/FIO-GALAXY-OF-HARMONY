using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [Header("Inputs: Weapons")]
    [SerializeField]
    private Transform weapons;

    [Header("What's going on at runtime?")]
    [SerializeField]
    private WeaponsEnum current;


    public void Select(WeaponsEnum selection)
    {
        current = selection;

        switch (selection)
        {
            case WeaponsEnum.Flute:
                SetVisible(current);
                break;
            case WeaponsEnum.Trompet:
                SetVisible(current);
                break;

            case WeaponsEnum.Violin:
                SetVisible(current);
                break;

            case WeaponsEnum.Guitar:
                SetVisible(current);
                break;

            case WeaponsEnum.Piano:
                SetVisible(current);
                break;
            case WeaponsEnum.Drumsticks:
                SetVisible(current);
                break;
        }
    }

    private void SetVisible(WeaponsEnum selection)
    {
        foreach (Transform _weapon in weapons)
        {

            if(_weapon.gameObject.name.ToLower() == selection.ToString().ToLower())
            {
                _weapon.gameObject.SetActive(true);
            }
            else
            {
                _weapon.gameObject.SetActive(false);
            }
        }
    }


}
