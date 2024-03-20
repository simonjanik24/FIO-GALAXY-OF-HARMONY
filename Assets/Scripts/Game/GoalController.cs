using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalController: MonoBehaviour 
{
    [SerializeField]
    private List<WeaponsEnum> weapons = new List<WeaponsEnum>();

    public List<WeaponsEnum> Weapons { get => weapons; set => weapons = value; }

}


