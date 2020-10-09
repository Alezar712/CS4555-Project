using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	float rotSpeed = 20f;
	Animator anim;

	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		

		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.UpArrow) )
		{
			anim.SetBool("Walk_Anim", false);
		}

		// walk backwards
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
			anim.SetBool("Walk_Anim", true);
		}

		else if (Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.LeftArrow) )
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
			anim.SetBool("Walk_Anim", true);
		}

		else if (Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.RightArrow) )
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}

}
