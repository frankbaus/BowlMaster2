using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<int> pins = new List<int>();

    private ActionMaster actionMaster = new ActionMaster();

	public void PinFall(int pinCount)
    {
        if(pinCount < 0 && pinCount > 10)
        {
            throw new UnityException("Invalid Pin Count");
        }
        pins.Add(pinCount);

        actionMaster.PinFalls(pins);

    }
}
