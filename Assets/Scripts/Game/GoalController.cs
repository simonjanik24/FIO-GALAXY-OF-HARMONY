using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalController: MonoBehaviour 
{
    [SerializeField]
    private List<WeaponsEnum> weapons = new List<WeaponsEnum>();
    public List<WeaponsEnum> Weapons { get => weapons; set => weapons = value; }

    private void Start()
    {
       // AddAll();
    }

    

    private void AddAll()
    {
        weapons.Add(WeaponsEnum.Guitar);
        weapons.Add(WeaponsEnum.Violin);
        weapons.Add(WeaponsEnum.Piano);
        weapons.Add(WeaponsEnum.Flute);
        weapons.Add(WeaponsEnum.Drumsticks);
        weapons.Add(WeaponsEnum.Trompet);
    }
}


