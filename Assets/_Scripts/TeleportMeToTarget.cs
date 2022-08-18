using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMeToTarget : MonoBehaviour
{
    public GameObject targetObject;
    public bool requiresButton;

    private bool isActive = false;

    private void Start()
    {
        if (!isActive && !requiresButton)
        {
            isActive = true;
        }
    }

    private void OnEnable()
    {
        if (requiresButton)
        {
            EventManager.TurnOn += Activate;
            EventManager.TurnOff += Deactivate;
        }
    }

    private void OnDisable()
    {
        if (requiresButton)
        {
            EventManager.TurnOn -= Activate;
            EventManager.TurnOff -= Deactivate;
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Player detected");
                CharacterController cc = other.gameObject.GetComponentInParent<CharacterController>();
                cc.enabled = false;
                other.gameObject.transform.position = targetObject.transform.position;
                Debug.Log(gameObject.name + " has been moved to " + targetObject.name);
                cc.enabled = true;
            }
        }
    }
}
