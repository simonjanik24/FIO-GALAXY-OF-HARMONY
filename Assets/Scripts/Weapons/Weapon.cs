using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponsEnum typ;

    public WeaponsEnum Typ { get => typ; set => typ = value; }

}
