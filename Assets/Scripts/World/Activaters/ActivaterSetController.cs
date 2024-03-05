using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivaterSetController : MonoBehaviour
{

    [Header("Inputs")]
    [SerializeField]
    private List<Activater> activaters;

    [Header("What's going on at runtime?")]
    private bool allActivated = false;
    private bool executed = false;

    [SerializeField]
    private List<UnityEvent> events;

    // Update is called once per frame
    void Update()
    {
        if (activaters != null)
        {
            if (activaters.Count > 0)
            {
                IsAllActivated();
            }
        }

        if (allActivated)
        {
            if(executed == false)
            {
                if(events != null)
                {
                    if(events.Count > 0)
                    {
                        foreach (UnityEvent _event in events)
                        {
                            _event.Invoke();
                        }
                        executed = true;
                    }
                }
                
            }
        }
    
    }


    private void IsAllActivated()
    {
        int shouldCount = activaters.Count;
        int isCount = 0;
        
        foreach (Activater activater in activaters)
        {
            if (activater.IsActivate())
            {
                isCount++;
            }
        }
   
        if(isCount == shouldCount)
        {
            allActivated = true;
        }
        else
        {
            allActivated = false;
        }
        
    }
}
