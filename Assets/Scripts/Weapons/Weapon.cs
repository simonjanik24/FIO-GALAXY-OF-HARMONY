using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField]
    private WeaponsEnum typ;

    public WeaponsEnum Typ { get => typ; set => typ = value; }


    public abstract void Shoot(float shootingPower, float impactPower);

}
