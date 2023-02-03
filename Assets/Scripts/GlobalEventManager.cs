using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static Action ReturnNamesAction;

    public static void ReturnNames() 
    {
        if (ReturnNamesAction != null)
            ReturnNamesAction.Invoke();
    }
}
