using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robo3 : MonoBehaviour
{
    /// <summary>
    ///  For RigidBody Component set a constraint for position on the y axis.
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

    // get position of y axis
    //float PositionY;
    //float PositionX;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.freezeRotation = true;

        rotationDirection();

        // Check distance between both objects
        print(EuclideanDistance());
        if (EuclideanDistance() < 6 && isNear == false) {
            activateAttack = true;
            isNear = true;
        }

        // if false move towards player, else attack
        if (activateAttack == false) {
            moveForwards();
        }

        if (activateAttack == true) {

            attack();
            //Destroy(rb);

            if (collided) { 
                
            }

        }

        rb.freezeRotation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot")) {
            collided = true;
        }
    }

    private void rotationDirection() {
        Vector3 pointingDirection = robot.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pointingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    
    private void moveForwards() {
        //rb.velocity = transform.forward * Time.deltaTime * 600f;
        //rb.AddRelativeForce(Vector3.forward * Time.deltaTime * 40f);
        /*
        float PositionY = gameObject.transform.position.y;
        float PositionX = gameObject.transform.position.x;

        transform.position += new Vector3(0, 0, transform.forward.z * Time.deltaTime * 8f);

        float PositionZ = gameObject.transform.position.z;
        
        transform.position = new Vector3(PositionX, PositionY, PositionZ);
        */
        transform.position += transform.forward * Time.deltaTime * 3f;
    }
    
    private void attack()
    {
        transform.position += transform.forward * Time.deltaTime * 5f;
    }

    private double EuclideanDistance()
    {
        return Math.Sqrt(((robot.transform.position.x - gameObject.transform.position.x) * (robot.transform.position.x - gameObject.transform.position.x)) + ((robot.transform.position.y - gameObject.transform.position.y) * (robot.transform.position.y - gameObject.transform.position.y)) + ((robot.transform.position.z - gameObject.transform.position.z) * (robot.transform.position.z - gameObject.transform.position.z)));
    }

} // End of Class
