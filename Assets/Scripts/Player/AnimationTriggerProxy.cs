using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerProxy : MonoBehaviour
{
    [SerializeField]
    private Shield shield;



    public void ActivateShield()
    {
        shield.Activate();
    }
}
