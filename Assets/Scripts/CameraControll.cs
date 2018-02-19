using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

    public Ball ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
		if(!ball)
        {
            Debug.LogError("No Ball Assigned");
            return;
        }

        offset = CalcBallOffset();
	}

    private Vector3 CalcBallOffset()
    {
        return this.transform.position - ball.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(ball.transform.position.z <= 1829f && CalcBallOffset().z != offset.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ball.transform.position.z + offset.z);
        }
	}
}
