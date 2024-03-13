using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalController: MonoBehaviour 
{ 

    [SerializeField]
    private Goals goals;

    public Goals Goals { get => goals; set => goals = value; }
}




[Serializable]
public class Goals
{
    [SerializeField]
    private List<WeaponsEnum> weapons;

    public List<WeaponsEnum> Weapons { get => weapons; set => weapons = value; }
}
