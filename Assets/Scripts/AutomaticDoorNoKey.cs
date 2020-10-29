using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorNoKey : MonoBehaviour
{
    enum State { doorIsOpen, doorIsClosed };
    State doorStatus = State.doorIsClosed;

    // reference Gameobject
    public GameObject sensorObject;

    // game object scripts
    private AutomaticDoorDetectionSystem sensorScript;

    // Start is called before the first frame update
    void Start()
    {
        sensorScript = sensorObject.GetComponent<AutomaticDoorDetectionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check everyframe if the sensor is being tiggered
        if (sensorScript.isActivated() && doorStatus == State.doorIsClosed)
        {
            // Temp code - slide door up
            transform.position += 2 * transform.TransformDirection(Vector3.up) * 0.02f * 10f * 8.5f;

            // the door is now open
            doorStatus = State.doorIsOpen;
        }
        else if (sensorScript.isActivated() == false && doorStatus == State.doorIsOpen)  {
            // Temp code - slide door down
            transform.position += 2 * transform.TransformDirection(Vector3.down) * 0.02f * 10f * 8.5f;

            // the door is now closed
            doorStatus = State.doorIsClosed;
        }
        
    }
}
