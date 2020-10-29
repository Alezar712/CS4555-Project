using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotingCube : MonoBehaviour
{
    //public GameObject myObject;
    //public GameObject explosionEffect;

    enum State { touching, notTouching, explodedSoundActive, explodedSoundnotActive };
    State status = State.notTouching;
    State explosion = State.explodedSoundnotActive;

    public float radius = 2.0f;
    public float power = 18.0f;

    AudioSource audioSource;
    [SerializeField] AudioClip explosionSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (status == State.touching)
        {
            Vector3 explosionPosition = transform.position;

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rigidBody = hit.GetComponent<Rigidbody>();

                if (rigidBody != null)
                {
                    rigidBody.AddExplosionForce(power, explosionPosition, radius, 3.0f);
                }
            }

            print("Explosion Happened");

            if (explosion == State.explodedSoundnotActive)
            {
                // display partical effect
                //Instantiate(explosionEffect, transform.position, transform.rotation);

                audioSource.Stop();
                audioSource.PlayOneShot(explosionSound);
                explosion = State.explodedSoundActive;
            }
            Invoke("removeObject", 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            print("touching bomb");
            status = State.touching;
        }

    }

    private void removeObject()
    {
        //myObject.SetActive(false);
        gameObject.SetActive(false);
    }

}

