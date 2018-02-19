using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    public float standingThreshold;

    public float distToRaise = 120.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsStanding()
    {
        float rotX = Mathf.Abs(gameObject.transform.eulerAngles.x);
        float rotZ = Mathf.Abs(gameObject.transform.eulerAngles.z);

        if (rotX > 180)
        {
            rotX = 360 - rotX;
        }

        if (rotZ > 180)
        {
            rotZ = 360 - rotZ;
        }

        return rotX <= standingThreshold && rotZ <= standingThreshold;
    }

    private void MovePin(float distanceToRaise, bool gravity)
    {
        Vector3 pinPos = transform.position;
        pinPos.y = distanceToRaise;
        transform.position = pinPos;

        rb.useGravity = gravity;
    }

    public void Raise()
    {
        if (IsStanding())
        {
            MovePin(distToRaise, false);
        }
    }

    public void Lower()
    {
        Debug.Log("Lower Pin " + name);
        MovePin(0, true);
    }


}
