using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorDetectionSystem : MonoBehaviour
{
    enum State { sensorActivated, sensorOff };
    State sensorStatus = State.sensorOff;

    private void OnTriggerEnter(Collider other)
    {
        // Any object with a box collider (other.attachedRigidbody)
        if (other.attachedRigidbody)
        {
           sensorStatus = State.sensorActivated;
           print($"Sensor is Activated: {sensorStatus}");
        }

    } // End of OnTriggerEnter

    // When the player has stopped touching the sensor
    private void OnTriggerExit(Collider other) {
        sensorStatus = State.sensorOff;
        print($"Sensor is not activated: {sensorStatus}");
    }

    public bool isActivated() {
        if (sensorStatus == State.sensorActivated) return true;
        else return false;
    }

}
