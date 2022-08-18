using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To Do:
///  + setup button to trigger individual objects
/// 
/// </summary>
public class Button : MonoBehaviour
{

    public bool stayOnAfterPress = false;
    public bool requiresInput = false;
    public bool activatesSpecifiedObject = false;
    public GameObject[] objectsToActivate;

    private EventManager em;
    private bool switchIsOn = false;

    public bool changesColorOnActivate = false;
    public Material onColor;
    public Material offColor;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        em = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!requiresInput)
            {
                ActivateObjects();
                ChangeMaterialColor(onColor);            // set color of button to "on color" material
                switchIsOn = true;
            }

            if (requiresInput && Input.GetKeyDown(KeyCode.F))
            {
                if (!switchIsOn)
                {
                    ActivateObjects();                 // tell the event manager to activate all the subscribed objects
                    ChangeMaterialColor(onColor);            // set color of button to "on color" material
                    switchIsOn = true;
                }
                else
                {
                    em.TurnOffObjects();
                    ChangeMaterialColor(offColor);            // set color of button to "off color" material
                    switchIsOn = false;
                }

            }
        }
    }

    private void ActivateObjects()
    {
        if (!activatesSpecifiedObject)
        {
            em.TurnOnObjects();                 // tell the event manager to activate all the subscribed objects
        }
        else
        {
            foreach(GameObject obj in objectsToActivate)
            {
                obj.SendMessage("Activate");
            }
        }
    }

    private void ChangeMaterialColor(Material color)
    {
        if (changesColorOnActivate)
        {
            rend.material = color;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!stayOnAfterPress)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!requiresInput)
                {
                    ChangeMaterialColor(offColor);
                    em.TurnOffObjects();
                }
            }
        }

    }
}
