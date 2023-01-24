using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalRules : MonoBehaviour
{
    public void AddFirstNewRule(Player[] Players)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            for (int j = 0; j < Players.Length; j++)
            {
                if (i != j && Players[i].StepNumber == Players[j].StepNumber)
                {
                    Players[i].HP -= 0.5f;
                    Players[j].HP -= 0.5f;
                }
            }
        }

        print("First rule has been activated");
    }

    private void AddSecondNewRule(Player[] Players, float avgNumber, ref int winner)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].StepNumber == Math.Round(avgNumber, 0))
            {
                winner = i;
                //TakeAwayHP();
            }
        }

        print("Second rule has been activated");
    }

    private void AddThirdNewRule(Player[] Players, ref int winner)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            for (int j = 0; j < Players.Length; j++)
            {
                if (Players[i].Active && Players[j].Active && i != j)
                {
                    if (Players[i].StepNumber == 0 && Players[j].StepNumber == 100)
                    {
                        winner = j;
                    }
                    else if (Players[i].StepNumber == 100 && Players[j].StepNumber == 0)
                    {
                        winner = i;
                    }
                }
            }
        }

        print("Third rule has been activated");
    }
}
