using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ActivateObject();
    public static event ActivateObject TurnOn;
    public static event ActivateObject TurnOff;

    public void TurnOnObjects()
    {
        if (TurnOn != null)
        {
            TurnOn();
        }
    }

    public void TurnOffObjects()
    {
        if (TurnOff != null)
        {
            TurnOff();
        }
    }
}
