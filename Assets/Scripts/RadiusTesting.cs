using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTesting : MonoBehaviour
{
    /*
        This is only for the drone gaurd
    */
    private float radius = 20f;
    private bool touching = false;
    private bool rangeStatus = false;

    private void Update()
    {
        if (!touching) { // false
            check();
        }
    }

    private void check() {
        Collider[] inRange = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in inRange)
        {
            if (hit.tag == "Robot")
            {
                touching = true;
                rangeStatus = true;
                print("You are touching");
                break;
            }
        }
    }

    public bool isInRange() { return rangeStatus; }
}
