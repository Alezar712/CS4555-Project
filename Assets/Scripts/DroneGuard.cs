using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneGuard : MonoBehaviour
{
    /// <summary>
    ///  add a sphere collider component
    /// </summary>

    // get the robot object
    public GameObject robot;
    private float rotationSpeed = 6f;

    // get GuardRobot Rigidbody compenent
    private Rigidbody rb;

    bool activateAttack = false;

    // Check if the gaurd is already destroyed
    bool isNear = false;

    // check if both objects collided
    bool collided = false;


    // get range script - checks if the player is in range (from current object)
    private RadiusTesting inRangeScript;

    // get health script from player
    private Health health;

    // get the animation from robot player
    Animator anim;
    private void Start()
    {
        health = robot.GetComponent<Health>();
        inRangeScript = gameObject.GetComponent<RadiusTesting>();
        anim = robot.GetComponent<Animator>();
    }

    private void Update()
    {
        //print($"ball status: {anim.GetBool("Open_Anim")}");
        if (inRangeScript.isInRange() == true)
        {
            activateProtocol();

            // we don't need this script anymore
            Destroy(inRangeScript);
        }

    } // End of Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            collided = true;
        }
    }

    private void rotationDirection()
    {
        Vector3 pointingDirection = robot.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pointingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void moveForwards()
    {
        transform.position += transform.forward * Time.deltaTime * 3f;
    }

    private void attack()
    {
        transform.position += transform.forward * Time.deltaTime * 5f;
    }

    private void activateProtocol()
    {
        rotationDirection();

        // Check distance between both objects
        if (EuclideanDistance() < 6 && isNear == false)
        {
            activateAttack = true;
            isNear = true;
        }

        // if false move towards player, else attack
        if (activateAttack == false)
        {
            moveForwards();
        }

        if (activateAttack == true)
        {
            attack();

            if (collided)
            {
                // remove one health point from player
                if (anim.GetBool("Open_Anim") == true) // true = not a ball
                {
                    health.instaDeath();
                }
                else
                {
                    print("Not damaged!");
                }

                // explode guardObject
                Destroy(gameObject); // Add Partical
            }

        }
    }

    private double EuclideanDistance()
    {
        return Math.Sqrt(((robot.transform.position.x - gameObject.transform.position.x) * (robot.transform.position.x - gameObject.transform.position.x)) + ((robot.transform.position.y - gameObject.transform.position.y) * (robot.transform.position.y - gameObject.transform.position.y)) + ((robot.transform.position.z - gameObject.transform.position.z) * (robot.transform.position.z - gameObject.transform.position.z)));
    }

} // End of Class
