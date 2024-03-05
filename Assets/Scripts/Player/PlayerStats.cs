using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float lifePoints = 100;

    public float LifePoints { get => lifePoints; set => lifePoints = value; }



    public void Damage(float damage)
    {

    }

    public void Heal()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
