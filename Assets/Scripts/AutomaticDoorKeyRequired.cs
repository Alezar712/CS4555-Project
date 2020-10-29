using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorKeyRequired : MonoBehaviour
{
    public GameObject keyObject;
    public GameObject sensorObject;

    // game object scripts
    private AutomaticDoorDetectionSystem sensorScript;
    private Key keyScript;

    enum State { locked, unlocked, alerted, notAlerted};
    State doorStatus = State.locked;

    // To alert the player he has not collect the key only once
    State doorAlert = State.notAlerted;


    // for door message
    public GameObject textmessage;
    public TextMesh textDoor;

    void Start()
    {
        // get scipt components
        sensorScript = sensorObject.GetComponent<AutomaticDoorDetectionSystem>();
        keyScript = keyObject.GetComponent<Key>();


        textDoor = textmessage.GetComponent<TextMesh>();
        textDoor.gameObject.SetActive(false);
        textDoor.text = "Key is required to open door!";
    }

    void Update()
    {
        // If the door is locked, otherwise do nothing
        if (doorStatus == State.locked) {
            // unlock the door if the key is collected
            unlockDoor();

            // reset status alert for the next time the player steps on the sensor and still does not have the key
            if (sensorScript.isActivated() == false) { doorAlert = State.notAlerted; textDoor.gameObject.SetActive(false); }
        }
    }

    private void unlockDoor() {
        // check that the player is stepping on the sensor
        // Check that the player collected the key
        if (keyScript.getKeyStatus() && sensorScript.isActivated())
        {
            // if the player collected the key and is stepping on the sensor, permanently unlock the door

            // Temp code - slide door up
            transform.position += 2 * transform.TransformDirection(Vector3.up) * 0.02f * 10f * 8.5f;
            doorStatus = State.unlocked;

            // These objects are no longer needed, once the door is unlocked it will remain unlocked
            Destroy(keyObject);
            Destroy(sensorObject);
            Destroy(textmessage);
        }
        else if (keyScript.getKeyStatus() == false && sensorScript.isActivated() && doorAlert == State.notAlerted) {
            // if the player is stepping on the sensor, but has not collected the key
            print("Player has not collected the key");
            textDoor.gameObject.SetActive(true);
            doorAlert = State.alerted;
        }
    }



}
