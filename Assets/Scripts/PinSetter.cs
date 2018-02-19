using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    public Text standingPinsText;
    private int lastSettledCount = 10;
    public int lastStandingCount = -1;
    public GameObject pinPrefab;

    private float lastChangeTime = 0f;
    private bool ballLeftBox = false;

    private Pin[] pins;
    private List<Vector3> pinPositions = new List<Vector3>();
    private Ball ball;
    private PinLayout pinLayout;
    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        pins = GameObject.FindObjectsOfType<Pin>();
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();

        pinLayout = GameObject.FindObjectOfType<PinLayout>();

        foreach (Pin p in pins)
        {
            pinPositions.Add(p.transform.position);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (ballLeftBox)
        {
            standingPinsText.color = Color.red;
            if (lastChangeTime == 0f)
            {
                lastChangeTime = Time.time;
            }
            CheckStanding();
        }
    }

    public void ResetBall()
    {
        ball.Reset();
    }

    public void RenewPins()
    {

       
        for (int i = 0; i < pins.Length; i++)
        {

            GameObject newPin = Instantiate(pinPrefab);
            Vector3 newPinPos = pinPositions[i];
            newPin.transform.position = newPinPos;
            newPin.transform.parent = pinLayout.transform;
            newPin.GetComponent<Rigidbody>().useGravity = false;
            pins[i] = newPin.GetComponent<Pin>();
        }

        lastSettledCount = 10;

    }

    void CheckStanding()
    {
        int tempCountStanding = CountStanding();
        standingPinsText.text = lastStandingCount.ToString();

        if (tempCountStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = tempCountStanding;
        }
        else if (Time.time - lastChangeTime > 3f)
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        ball.Reset();
        standingPinsText.color = Color.green;
        lastSettledCount -= CountStanding();
        ActionMaster.Action action = actionMaster.Bowl(lastSettledCount);
        Debug.Log(action + " " + lastSettledCount);
        if(action.Equals(ActionMaster.Action.TIDY))
        {
            animator.SetTrigger("tidyTrigger");
        } else
        {
            lastSettledCount = 10;
            animator.SetTrigger("resetTrigger");
        }
        
        lastStandingCount = -1;
        ballLeftBox = false;
    }

    int CountStanding()
    {
        int pinsStanding = 0;

        foreach (Pin pin in pins)
        {
            if (pin && pin.IsStanding())
            {
                pinsStanding++;
            }
        }

        return pinsStanding;
    }

    public void RaisePins()
    {
        foreach (Pin pin in pins)
        {
            if (pin)
            {
                pin.Raise();
            }
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in pins)
        {
            if (pin)
            {
                pin.Lower();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponentInParent<Pin>())
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

    public void SetBallLeftbox(int left)
    {
        ballLeftBox = left == 1;
    }
}