using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationController : MonoBehaviour
{

    private Coroutine currentCoroutine;

    public void SetVibration(float lowFrequency, float highFrequency)
    {
        // Rumble the  low-frequency (left) motor at 1/4 speed and the high-frequency
        // (right) motor at 3/4 speed.
        Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
    }

    public void SetVibrationByTime(float lowFrequency, float highFrequency, float time)
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SetVibrationByTimeEnumerator(lowFrequency, highFrequency, time));
    }

    private IEnumerator SetVibrationByTimeEnumerator(float lowFrequency, float highFrequency, float time)
    {
        Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        yield return new WaitForSeconds(time);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }


    public void PauseHaptics()
    {
        // Pause haptics globally.
        InputSystem.PauseHaptics();
    }

    public void ResumeHaptics()
    {
        // Resume haptics globally.
        InputSystem.ResumeHaptics();
    }

    public void ResetHaptics()
    {
        // Stop haptics globally.
        InputSystem.ResetHaptics();
    }


    

    


}
