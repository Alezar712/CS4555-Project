using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePressurePlate : MonoBehaviour
{
    enum State { pressed, notPressed };
    State status = State.notPressed;

    private float movementSpeed = 2f;

    public int patternValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && status == State.notPressed) { // Any object with a box collider (other.attachedRigidbody)
            // lowers the object, showing that it was pressed.
            transform.position += 2 * transform.TransformDirection(Vector3.down) * 0.02f * movementSpeed * 2.5f;
            status = State.pressed;
        }

    }

    public bool isPressed() {
        if (status == State.pressed) { return true; }
        else { return false; }
    }
    public void setStatusToNotPressed() {
        status = State.notPressed;
    }
    public void setToOriginalPosition() {
        transform.position += 2 * transform.TransformDirection(Vector3.up) * 0.02f * movementSpeed * 2.5f;
    }

    // Testing Code
    public void printValue() {
        print($"The status is {status}");
    }



}
