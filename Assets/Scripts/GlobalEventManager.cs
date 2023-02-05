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
}
