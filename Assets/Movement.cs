using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jump = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }

        else if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.UpArrow) )
		{
			
		}

        if (Input.GetKey(KeyCode.Space)) {
            transform.position += transform.TransformDirection (Vector3.up) * Time.deltaTime * jump * 3f;
        }

        else if (Input.GetKeyUp(KeyCode.Space))
		{
			
		}
    }
}
