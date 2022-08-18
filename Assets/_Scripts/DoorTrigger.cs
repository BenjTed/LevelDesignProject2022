using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool requiresInput = false;
    public bool staysOpen = false;
    private bool isColliding = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("isOpen", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!requiresInput)
            {
                anim.SetBool("isOpen", true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!staysOpen)
        {
            anim.SetBool("isOpen", false);
        }

        isColliding = false;
    }
}
