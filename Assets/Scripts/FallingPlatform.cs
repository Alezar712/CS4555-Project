using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    enum State { NotFallen, Fallen };

    State fallen = State.NotFallen;

    private void OnTriggerEnter(Collider other)
    {
        if (fallen != State.Fallen)
        {
            if (other.attachedRigidbody)
            {      
                print("Platform will fall");
                fallen = State.Fallen;
                Invoke("enableGravityFall", 4f);

                // 4 seconds after the object starts falling, destroy the object
                Invoke("destroyFallingPlatform", 8f);
            }
        }
    }

    private void enableGravityFall()
    {
        print("Platform fell");
        //fallingObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<Rigidbody>();
    }
    private void destroyFallingPlatform() {
        Destroy(gameObject);
    }

}
