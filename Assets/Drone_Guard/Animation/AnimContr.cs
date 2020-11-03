using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("1"))
        {
            anim.Play("ShutDown");
        }

        if (Input.GetKeyDown("2"))
        {
            anim.Play("WakeUp");
        }

        if (Input.GetKeyDown("3"))
        {
            anim.Play("Destroyed");
        }

    }
}
