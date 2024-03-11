using PathCreation;
using UnityEngine;

public class Flute : Weapon
{
    [SerializeField]
    private Transform particleSystem;


    public void FlipRight()
    {
        Vector3 eulerRotation = new Vector3(0, 0, -58.4f);
        Quaternion desiredRotation = Quaternion.Euler(eulerRotation);
        particleSystem.localRotation = desiredRotation;
    }

    public void FlipLeft()
    {
        Vector3 eulerRotation = new Vector3(0, 0, 58.4f);
        Quaternion desiredRotation = Quaternion.Euler(eulerRotation);
        particleSystem.localRotation = desiredRotation;
    }

    public override void Shoot(float shootingPower, float hitPower)
    {
        throw new System.NotImplementedException();
    }

}
