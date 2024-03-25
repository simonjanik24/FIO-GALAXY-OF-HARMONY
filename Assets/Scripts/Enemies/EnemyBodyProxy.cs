using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script is a proxy to reference the Enemy script in order to execute the collision inside of the Hit script, where hits of the player are detected.
 * This script is needed because sometimes the colliders are not laying on the parent enemy gameobject, but on many different body parts.
 * Each body part should have this script attached and reference to the parent enemy gameobject script "Enemy"
 */
public class EnemyBodyProxy : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    public Enemy Enemy { get => enemy; set => enemy = value; }
}
