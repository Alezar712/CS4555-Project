using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    // Get the text object
    public GameObject textObject;
    private TextMesh text;

    private bool isAlerted = false;
    public int health = 4;

    private void Start()
    {
        textObject.SetActive(false);
        text = textObject.GetComponent<TextMesh>();
    }

    private void Update()
    {
        if (health < 1 && isAlerted == false)
        {

            print("You are dead!");
            isAlerted = true;

            // Display text
            textObject.SetActive(true);
            text.color = Color.red;
            text.text = $"You are DEAD...";

            /*
                respawn code - reload scene after 2 seconds of being dead
                Invoke("SomeMethod", 3f);
            */
            Invoke("reLoadScene", 1.2f);

        }
    }

    public void damage()
    {
        if (health >= 1)
        {
            health -= 1;

            if (!(health < 1))
            {
                print($"Health: {health}");

                // Display text
                textObject.SetActive(true);
                text.text = $"Health: {health}";

                // disable text after n-seconds
                Invoke("disableText", 2f);
            }
        }
    }

    private void reLoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void instaDeath()
    {
        health = 0;
        print($"Health: {health}");
    }
    private void disableText()
    {
        textObject.SetActive(false);
    }
}
