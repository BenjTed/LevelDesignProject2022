using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ToDo:
/// + update direction to use enum (drop down box in inspector)
/// + detect multiple objects
/// 
/// *** YOU MUST TURN ON "AUTO SYNC TRANSFORMS" IN THE PHYSICS SETTINGS IN UNITY FOR THIS SCRIPT TO WORK CORRECTLY
/// 
/// </summary>


public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 10f;
    private bool isMoving = false;
    private bool isActive = false;

    public bool requiresPlayerCollision = false;
    public bool requiresButton = false;
    public bool usesCheckpoints = false;
    public GameObject[] checkpointObjects;
    private int currentCheckpoint;

    public Vector3 direction;
    private Vector3 velocity;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (usesCheckpoints)
        {
            // work out distance between platform and checkpoint objects
            float dist = Vector3.Distance(checkpointObjects[currentCheckpoint].transform.position, transform.position);

            if (dist > 0.25f)
            {
                direction = checkpointObjects[currentCheckpoint].transform.position - transform.position;
                direction.Normalize();
            }
            else
            {
                if (currentCheckpoint < checkpointObjects.Length - 1)
                {
                    currentCheckpoint++;
                }
                else
                {
                    currentCheckpoint = 0;
                }

            }
        }

        if (requiresButton)
        {
            if (isActive)
            {
                MovePlatform();
            }
        }
        else
        {
            MovePlatform();
        }

    }

    private void MovePlatform()
    {
        if (!requiresPlayerCollision)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            velocity = moveSpeed * direction * Time.deltaTime;
            transform.parent.Translate(velocity);

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
        Debug.Log(other.name + " detected.");
        other.transform.parent = transform;
        isMoving = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name + " detected. OnTriggerStay");
        other.transform.parent = transform;
        isMoving = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isMoving = false;
        other.transform.parent = null;
    }
}
