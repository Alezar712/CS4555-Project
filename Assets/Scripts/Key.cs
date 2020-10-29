using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    enum State { collected, notCollected }
    State keyStatus = State.notCollected;

    private void OnTriggerEnter(Collider other)
    {
        // the key is collected by robot
        if ( other.gameObject.CompareTag("Robot") ) {
            keyStatus = State.collected;
            gameObject.SetActive(false);
            print( "Key is collected" );
        }
    }

    public bool getKeyStatus() {
        if (keyStatus == State.notCollected) { return false; }
        else { return true; }
    }

}
