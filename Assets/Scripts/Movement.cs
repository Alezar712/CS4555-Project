using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jump = 2;

    enum State { Alive, Dead, NotStun, TempStun };
    State state = State.NotStun; // By default the robot is not stunned
    State healthStatus = State.Alive; // By default the robot is Alive

    AudioSource audioSource;
    [SerializeField] AudioClip deadSound;

    Animator anim;

    //Rigidbody rigidBody;
    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerScript playerScript = thePlayer.GetComponent<PlayerScript>(); Get variables from another script
        anim = gameObject.GetComponent<Animator>();

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // to exit out of the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (state == State.NotStun && healthStatus == State.Alive)
        { // check if he is stunned



            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += 2 * transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 3f;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {

            }

            // Walk Backwards
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
            }



            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += transform.TransformDirection(Vector3.up) * Time.deltaTime * jump * 3f;
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            print("Pickup");

        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            print("Touched Tap");

            state = State.TempStun;

            // so that the robot is no longer making contact with the Trap
            transform.position -= 10 * transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;

            anim.SetBool("Open_Anim", false);

            Invoke("stunMainCharacter", 10f);

        }
        else if (other.gameObject.CompareTag("OutOfBounce"))
        {
            print("You are Dead");

            healthStatus = State.Dead;

            audioSource.Stop();
            audioSource.PlayOneShot(deadSound);

            Invoke("LoadFirstLevel", 2f);

        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            print("Touch Enemy");

            //transform.position -= 170 * transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
            //rigidBody.AddRelativeForce( (-Vector3.forward * 5000) );
            //rigidBody.AddRelativeForce(Vector3.up * 20);

            rigidBody.AddRelativeForce((new Vector3(60, 60, 0)) * 12);
        }


    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void stunMainCharacter()
    {
        state = State.NotStun;
        anim.SetBool("Open_Anim", true);
    }

    public void changeStateStunned()
    {
        state = State.TempStun;
    }
    public void changeStateNotStunned()
    {
        state = State.NotStun;
    }

}
