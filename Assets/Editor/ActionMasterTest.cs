using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreTest
{
    private ActionMaster.Action endturn = ActionMaster.Action.ENDTURN;
    private ActionMaster.Action endgame = ActionMaster.Action.ENDGAME;
    private ActionMaster.Action tidy = ActionMaster.Action.TIDY;
    private ActionMaster.Action reset = ActionMaster.Action.RESET;
    private ActionMaster actionMaster;

    private int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1
        };

    [SetUp]
    public void SetUp()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T00InvalidPinCount()
    {
        Assert.Throws<UnityException>(() => actionMaster.Bowl(-1));
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endturn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03Bowl2TimesReturnsTidyEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endturn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame()
    {

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T05CheckStrikeInLastFrameAllows2MoreBowls()
    {
        int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1,
            10, 0
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(endgame, actionMaster.Bowl(10));
    }

    [Test]
    public void T06CheckStrikeInLastFrame20FrameTidy()
    {
        int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1,
            10
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(2));
    }

    [Test]
    public void T07BowlSpareIn20Frame()
    {
        int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1,
            1
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T08Bowl1In20Frame()
    {
        int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1,
            1
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(endgame, actionMaster.Bowl(1));
    }

    [Test]
    public void T09TidyAfterStrikeIn19AndNoStrikeIn20()
    {
        int[] bowls10Frame = {
            5, 3,
            10,
            10,
            2, 8,
            3, 3,
            5, 3,
            7, 1,
            8, 2,
            9, 1,
            10
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T10NathansTest()
    {
        int[] bowls10Frame = {
            0, 10,
            5
        };

        foreach (int bowlResult in bowls10Frame)
        {
            actionMaster.Bowl(bowlResult);
        }

        Assert.AreEqual(endturn, actionMaster.Bowl(1));
    }

    [Test]
    public void T11Dondi10thFrameTurkey()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endgame, actionMaster.Bowl(10));
    }
}