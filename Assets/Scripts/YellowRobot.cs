using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowRobot : MonoBehaviour
{
    /*
     set laymask to "Robot"
     */

    public GameObject robot;

    private bool collided = false;

    public LayerMask robotLayerMask;

    // so that the enemy can only stun the player every n-ammount of seconds
    private bool stunReady = true;

    // get the Movement.cs to disable movement if stunned;
    private Movement movementScript;

    // disable the player
    private bool isDisabled = false;

    // enemy rotation speed
    private float rotationSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = robot.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        // is within radius, is not colliding and is not disabled
        if ( Physics.CheckSphere(transform.position, 7f, robotLayerMask) && !collided && !isDisabled) { // only when player is not colliding with robot ( !collided )
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
        if (collided && stunReady) {

            // Attack code
            // #####################################################

            // if the player collides with the robot, temp stun him
            // The player will not be able to move
            // stun player
            movementScript.changeStateStunned();

            // unstunn player after n-amount of seconds
            Invoke("unstunnedPlayer", 6f);

            // End of Attack code
            // #####################################################

            // disable robot and stun ability
            stunReady = false;
            isDisabled = true;

            // rebot robot and stun abilty after n-seconds
            Invoke("resetRobot", 12f);

        }

    }

    private void resetRobot() {
        stunReady = true;
        isDisabled = false;
        gameObject.GetComponent<EnemyAI>().enabled = true;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }
    private void unstunnedPlayer() {
        movementScript.changeStateNotStunned();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot")) {
            collided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collided = false;
    }

    private void moveTowardsPlayer() {
        transform.position += transform.forward * Time.deltaTime * 3f;
    }

    private void rotationDirection()
    {
        Vector3 pointingDirection = robot.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pointingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

}
