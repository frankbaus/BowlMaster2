using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{

    public enum Action { TIDY, RESET, ENDTURN, ENDGAME };


    private int[] bowls = new int[21];

    public ActionMaster.Action PinFalls(List<int> pins)
    {

        int pinsLen = pins.Count;
        int lastPinCount = pins[pinsLen - 1];

        if (pinsLen == 21)
        {
            return Action.ENDGAME;
        }

        if (pinsLen == 19 && lastPinCount == 10)
        {
            pinsLen += 1;
            return Action.RESET;
        }

        //20 frame spare
        if (last2FramesAllowed())
        {
            pinsLen += 1;
            if (bowls[20 - 1] > 0 && (bowls[19 - 1] + bowls[20 - 1]) % 10 == 0)
            {
                return Action.RESET;
            }
            return Action.TIDY;
        }

        //no spare in 20 frame
        if (pinsLen == 20 && !last2FramesAllowed())
        {
            return Action.ENDGAME;
        }

        if (pinsLen % 2 == 0)
        {
            pinsLen += 1;
            return Action.ENDTURN;
        }

        if (pinsLen % 2 != 0)
        {
            if (lastPinCount == 10)
            {
                pinsLen += 2;
                return Action.ENDTURN;
            }
            else
            {
                pinsLen += 1;
                return Action.TIDY;
            }
        }

        throw new UnityException("Not sure what to do " + pinsLen);
    }

    private bool last2FramesAllowed()
    {
        return bowls[19 - 1] + bowls[20 - 1] >= 10;
    }
}
