using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]

public class DragLaunch : MonoBehaviour
{

    private Ball ball;
    private Lane lane;

    private Vector3 mousePosStart;
    private float timeStart, timeEnd;
    private float laneWidth, ballWidth, ballClampVal;


    // Use this for initialization
    void Start()
    {
        ball = GetComponent<Ball>();
        lane = GameObject.FindObjectOfType<Lane>();

        laneWidth = lane.transform.localScale.x / 2;
        ballWidth = ball.transform.localScale.x / 2;

        ballClampVal = laneWidth - ballWidth;
    }

    public void DragStart()
    {
        // Capture Time and Position of drag start
        Debug.Log("DragStart Pos: " + Input.mousePosition);
        Debug.Log("DragStart Time: " + Time.timeSinceLevelLoad);
        mousePosStart = Input.mousePosition;
        timeStart = Time.time;
    }

    public void DragEnd()
    {
        //launch the ball
        Vector3 mousePosEnd = Input.mousePosition;
        timeEnd = Time.time;

        Vector3 distance = (mousePosEnd - mousePosStart);
        float duration = (timeEnd - timeStart);

        Vector3 speed = new Vector3(distance.x / duration, 0, distance.y / duration);

        ball.Launch(Vector3.Scale(speed, new Vector3(0.1f, 0, .5f)));
    }

    public void MoveStart(float xNudge)
    {

        Debug.Log(timeEnd);
        if(timeEnd > 0.0f)
        {
            return;
        }
        Vector3 newBallPos = ball.transform.position;
        newBallPos.x += xNudge;

        newBallPos.x = Mathf.Clamp(newBallPos.x, -ballClampVal, ballClampVal);

        ball.transform.position = newBallPos;
    }

}
