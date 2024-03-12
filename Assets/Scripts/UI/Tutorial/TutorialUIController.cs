using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerUIIcons))]
public class TutorialUIController : MonoBehaviour
{
    [SerializeField]
    private ComputingPlatform computingPlatform;
    [SerializeField]
    private List<GameObject> xboxControlMessages;

    private List<GameObject> messages;

    private void Awake()
    {
        if(computingPlatform == ComputingPlatform.PC)
        {
            if(xboxControlMessages != null && xboxControlMessages.Count > 0)
            {
                messages = xboxControlMessages;
            }
            
        }else if (computingPlatform == ComputingPlatform.Xbox)
        {
            if (xboxControlMessages != null && xboxControlMessages.Count > 0)
            {
                messages = xboxControlMessages;
            }
        }
        else if (computingPlatform == ComputingPlatform.Playstation)
        {

        }
        else if (computingPlatform == ComputingPlatform.Nintendo)
        {

        }
    }


    public void OnlyShow(string name)
    {
        foreach (GameObject message in messages)
        {
            if(message.name == name)
            {
                message.SetActive(true);
            }
            else
            {
                message.SetActive(false);
            }
        }
    }
}