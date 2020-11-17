using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRobot : MonoBehaviour
{
    /*
     set laymask to "Robot"
     */

    public GameObject robot;

    private bool collided = false;

    public LayerMask robotLayerMask;

    // so that the enemy can only stun the player every n-ammount of seconds
    private bool attackReady = true;

    // disable the player
    private bool isDisabled = false;

    // enemy rotation speed
    private float rotationSpeed = 8f;

    // Params to push player
    public float radius = 4.0f;
    public float power = 500.0f;


    // Update is called once per frame
    void Update()
    {
        // is within radius, is not colliding and is not disabled
        if (Physics.CheckSphere(transform.position, 7f, robotLayerMask) && !collided && !isDisabled)
        { // only when player is not colliding with robot ( !collided )
            gameObject.GetComponent<EnemyAI>().enabled = false;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            rotationDirection();
            moveTowardsPlayer();
            
        }

        //need fix not efficient
        if (Physics.CheckSphere(transform.position, 7f, robotLayerMask) == false && !collided && !isDisabled)
        {
            gameObject.GetComponent<EnemyAI>().enabled = true;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }

        // if it is colliding and his stun is charged
        if (collided && attackReady)
        {

            // Attack code
            // #####################################################

            // if the player collides with the robot
            //robot.transform.position += robot.transform.TransformDirection(-Vector3.forward) * 5;

            Rigidbody rigidBody = robot.GetComponent<Rigidbody>();
            rigidBody.AddExplosionForce(power, robot.transform.position , radius, 5.0f);
            
            // End of Attack code
            // #####################################################

            // disable robot and stun ability
            attackReady = false;
            isDisabled = true;

            // rebot robot and stun abilty after n-seconds
            Invoke("resetRobot", 7f);

        }

    }

    private void resetRobot()
    {
        attackReady = true;
        isDisabled = false;
        gameObject.GetComponent<EnemyAI>().enabled = true;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            collided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collided = false;
    }

    private void moveTowardsPlayer()
    {
        transform.position += transform.forward * Time.deltaTime * 3f;
    }

    private void rotationDirection()
    {
        Vector3 pointingDirection = robot.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pointingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

}

