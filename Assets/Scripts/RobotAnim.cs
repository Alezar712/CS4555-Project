using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour
{

	Vector3 rot = Vector3.zero;
	float rotSpeed = 30f;
	Animator anim;

	private bool startTimer = false;
	private float timer = 12.0f;
	private bool clickedOnce = true;

	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	// Update is called once per frame
	void Update()
	{
		if (startTimer == true) { timer -= Time.deltaTime; }


		CheckKey();

		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		// close with no roll animation
		openAnimation();

		// Walk
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
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

		else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
			anim.SetBool("Walk_Anim", true);
		}

		else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
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

	}

	// new
	private void openAnimation()
	{
		if (timer <= 0 && !anim.GetBool("Open_Anim"))
		{ // timer is less than zero and robot is a ball
			anim.SetBool("Open_Anim", true);
			startTimer = false;
			Invoke("resetTimer", 12.0f);
		}

		// Close - no roll
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (!anim.GetBool("Open_Anim") && clickedOnce) // if its closed, open
			{
				clickedOnce = false;
				anim.SetBool("Open_Anim", true);
				startTimer = false;
				Invoke("resetTimer2", 12.0f);
			}
			else if (timer >= 12.0f && anim.GetBool("Open_Anim")) // if it is open, close
			{// timer was reset and you can now close again
				anim.SetBool("Open_Anim", false);
				startTimer = true;
			}
		}
	}

	// new 
	private void resetTimer()
	{
		timer = 12.0f;
		//print("Time reseted.");
	}

	// new
	private void resetTimer2()
	{
		timer = 12.0f;
		clickedOnce = true;
		//print("Time reseted 2.");
	}
}