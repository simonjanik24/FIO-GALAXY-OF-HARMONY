using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeToMusic : MonoBehaviour
{
    public float factor = 2;

    

    public bool minMaxScale;
    public float extraScaleX;
    public float extraScaleY;
    [SerializeField]
    private float reactivness = 0.1f;

    [SerializeField]
    private bool bass = true;
    [SerializeField]
    private bool nb = true;
    [SerializeField]
    private bool middles = true;
    [SerializeField]
    private bool highs = true;


    private Vector3 startSize;


    private void Awake()
    {
        startSize = transform.localScale;
    }

    private void FixedUpdate()
    {
        if(MusicController.instance != null)
        {
            makeObjectsShakeScale();
        }
        
    }

    private void makeObjectsShakeScale()
    {
        if (bass)
        {
            float frequencyBass = MusicController.instance.getFrequenciesDiapson(0, 7, 10) * factor;
            ScaleObject(transform, frequencyBass);
        }
        if (nb)
        {
            float frequencyNB = MusicController.instance.getFrequenciesDiapson(7, 15, 100) * factor;
            ScaleObject(transform, frequencyNB);
        }
        if (middles)
        {
            float frequencyMiddles = MusicController.instance.getFrequenciesDiapson(15, 30, 200) * factor;
            ScaleObject(transform, frequencyMiddles);
        }
        if (highs)
        {
            float frequencyHighs = MusicController.instance.getFrequenciesDiapson(30, 32, 1000) * factor;
            ScaleObject(transform, frequencyHighs);
        }
       
       
    }

    private void ScaleObject(Transform obj, float frequency)
    {

        if (minMaxScale)
        {
            float scaledFrequencyX = Mathf.Clamp(frequency, startSize.x, startSize.x + extraScaleX);
            float scaledFrequencyY = Mathf.Clamp(frequency, startSize.y, startSize.y + extraScaleY);
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaledFrequencyX, scaledFrequencyY, 1), reactivness);
        }
        else
        {
            float scaledFrequency = frequency;
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(scaledFrequency, scaledFrequency, 1), reactivness);
        }
        

        
    }
}
