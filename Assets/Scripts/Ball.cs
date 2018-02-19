using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    public Vector3 launchVelocity;
    Rigidbody rb;
    AudioSource audioSource;
    private Vector3 initPosition;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initPosition = transform.position;
        rb.useGravity = false;
    }

    public void Launch(Vector3 velocity)
    {
        rb.useGravity = true;
        rb.velocity = velocity;
        audioSource.Play();
    }

    public void Reset()
    {
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = initPosition;
    }
}
