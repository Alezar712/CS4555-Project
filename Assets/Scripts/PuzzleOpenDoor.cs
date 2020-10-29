using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PuzzleOpenDoor : MonoBehaviour
{
    enum State { pressed, notPressed, locked, notLocked };
    State obj1Satus = State.notPressed;
    State obj2Satus = State.notPressed;
    State obj3Satus = State.notPressed;
    State doorSatus = State.locked;

    // reference Gameobject
    public GameObject myObject1;
    public GameObject myObject2;
    public GameObject myObject3;
    public GameObject robot;

    // game object scripts
    private PuzzlePressurePlate obj1Script;
    private PuzzlePressurePlate obj2Script;
    private PuzzlePressurePlate obj3Script;

    // puzzle pattern
    int[] correctPattern = { 3, 1, 2 };

    ArrayList userinput = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        obj1Script = myObject1.GetComponent<PuzzlePressurePlate>();
        obj2Script = myObject2.GetComponent<PuzzlePressurePlate>();
        obj3Script = myObject3.GetComponent<PuzzlePressurePlate>();
    }

    // Update is called once per frame
    void Update()
    {
        // Every Frame check if any of the objects is pressed!
        if (obj1Script.isPressed() == true && obj1Satus == State.notPressed)
        {
            userinput.Add(obj1Script.patternValue);
            obj1Satus = State.pressed;
            print($"Value of this object is {obj1Script.patternValue}");
        }
        else if (obj2Script.isPressed() == true && obj2Satus == State.notPressed)
        {
            userinput.Add(obj2Script.patternValue);
            obj2Satus = State.pressed;
            print($"Value of this object is {obj2Script.patternValue}");
        }
        else if (obj3Script.isPressed() == true && obj3Satus == State.notPressed)
        {
            userinput.Add(obj3Script.patternValue);
            obj3Satus = State.pressed;
            print($"Value of this object is {obj3Script.patternValue}");
        }


        // only check the the array and Arraylist have the same amount of elements
        if (correctPattern.Length == userinput.Count)
        {
            doorSystem();
            //Testing code
            print($"ArrayList size {userinput.Count}");
        }



    }

    private void doorSystem()
    {
        // check if the pattern is correct
        // if it is correct permently unlock the door
        if (isCorrect(userinput) == true && doorSatus == State.locked)
        {
            print("The Pattern is correct!");

            // move door up
            // Temp code
            transform.position += 2 * transform.TransformDirection(Vector3.up) * 0.02f * 10f * 8.5f;
            print($"Time.deltaTime is {Time.deltaTime}");

            // set the door status to unlocked
            doorSatus = State.notLocked;
        }
        else if (isCorrect(userinput) == false && doorSatus == State.locked)
        {
            // if it is not correct, reset all the inputs 
            print("The Pattern is not Correct Lol");

            // push the player back so that he does not touch the pressure plates while they are reseting (Camera is off)
            robot.transform.position += 2 * transform.TransformDirection(Vector3.right) * 0.02f * 3f * 15f;

            userinput = new ArrayList();

            // reset pressure plate 1 to original position
            obj1Script.setToOriginalPosition();
            // reset status for pressure plate 1
            obj1Script.setStatusToNotPressed();
            //obj1Script.printValue();

            // reset pressure plate 2 to original position
            obj2Script.setToOriginalPosition();
            // reset status for pressure plate 2
            obj2Script.setStatusToNotPressed();

            // reset pressure plate 3 to original position
            obj3Script.setToOriginalPosition();
            // reset status for pressure plate 3
            obj3Script.setStatusToNotPressed();

            obj1Satus = State.notPressed;
            obj2Satus = State.notPressed;
            obj3Satus = State.notPressed;
        }
    }

    private bool isCorrect(ArrayList userinput)
    {
        for (int i = 0; i <= correctPattern.Length - 1; i++)
        {
            //print($"Array: {correctPattern[i]},  ArrayList: {(int)userinput[i]}");
            if ((int)userinput[i] != correctPattern[i])
            {
                return false;
            }
        }
        return true;
    }

}