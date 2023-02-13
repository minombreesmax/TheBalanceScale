using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent ReturnNamesEvent = new UnityEvent();
    public static UnityEvent StartTimerEvent = new UnityEvent();
    public static UnityEvent StopTimerEvent = new UnityEvent();
<<<<<<< HEAD
    public static UnityEvent RoundSoundEvent = new UnityEvent();
    public static UnityEvent ConvertTextToSpeachEvent = new UnityEvent();
=======
>>>>>>> parent of 3b49ed2 (Added new sounds, features and base for future updates)

    public static void ReturnNames() 
    {
        ReturnNamesEvent.Invoke();
    }

    public static void StartTimer() 
    {
        StartTimerEvent.Invoke();
    }

    public static void StopTimer()
    {
        StopTimerEvent.Invoke();
    }
<<<<<<< HEAD

    public static void RoundSound() 
    {
        RoundSoundEvent.Invoke();
    }

    public static void ConvertTextToSpeach() 
    {
        ConvertTextToSpeachEvent.Invoke();
    }

=======
>>>>>>> parent of 3b49ed2 (Added new sounds, features and base for future updates)
}
