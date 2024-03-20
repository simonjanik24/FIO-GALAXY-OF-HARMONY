using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ControllerUIIcons))]
public class NotificationController : MonoBehaviour
{
    [SerializeField]
    private ComputingPlatform computingPlatform;
    [SerializeField]
    private Transform xboxControlMessagesParent;

    private Transform messagesParent;

    private AudioSource audioSource;

    private WeaponWheelController weaponWheelController;
    private WeaponController weaponController;

    public ComputingPlatform ComputingPlatform { get => computingPlatform; set => computingPlatform = value; }

    private void Awake()
    {
        if(computingPlatform == ComputingPlatform.PC)
        {
            if(xboxControlMessagesParent != null && xboxControlMessagesParent.childCount > 0)
            {
                messagesParent = xboxControlMessagesParent;
            }
            
        }else if (computingPlatform == ComputingPlatform.Xbox)
        {
            if (xboxControlMessagesParent != null && xboxControlMessagesParent.childCount > 0)
            {
                messagesParent = xboxControlMessagesParent;
            }
        }
        else if (computingPlatform == ComputingPlatform.Playstation)
        {

        }
        else if (computingPlatform == ComputingPlatform.Nintendo)
        {

        }

        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
      /*  if(!weaponWheelController.IsOpen && weaponController.Current == WeaponsEnum.Flute)
        {

        }*/
    }

    public void Show(string name)
    {
        if (messagesParent != null && messagesParent.childCount > 0)
        {
            foreach (Transform child in messagesParent)
            {
                if (child.gameObject.name == name)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
        }

    }

    public void DestroyBy(string name)
    {
        if (messagesParent != null && messagesParent.childCount > 0)
        {
            foreach (Transform child in messagesParent)
            {
                if (child.gameObject.name == name)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
        }
    }
}