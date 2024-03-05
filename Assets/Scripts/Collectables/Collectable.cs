using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour
{
    public abstract void CollectMe();

   

}
