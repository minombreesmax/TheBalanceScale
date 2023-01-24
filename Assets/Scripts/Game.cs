using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] protected Text AvgNumberText, RoundWinnerText, RoundText;
    [SerializeField] protected Player[] Players;
    [SerializeField] protected GameObject NumberInput;
    [SerializeField] protected Button RestartButton;

    protected int round = 0, winner;
    protected float avgNumber;
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    protected void GameStart() 
    {
        RestartButton.gameObject.SetActive(false);
        SetPlayersAuto();
        SetIsPlayerBot(DataHolder.IsPlayerBot);
        Players[0].Name = "You";
    }

    protected void RandNumeric() 
    {
        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].isBot)
            {
                Players[i].StepNumber = UnityEngine.Random.Range(0, 101);
                Players[i].ValueText.text = $"{Players[i].Name}\n{Players[i].StepNumber}";
            }
        }
    }

    protected void CountAvgNumber() 
    {
        avgNumber = 0;

        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].Active)
                avgNumber += Players[i].StepNumber;
        }

        avgNumber = (avgNumber / ActivePlayerCount()) * 0.8f;
        AvgNumberText.text = $"{avgNumber}";
    }

    protected void GetRoundWinner() 
    {
        winner = 0;
        var difference = Mathf.Abs(Players[0].StepNumber - avgNumber);

        for(int i = 1; i < Players.Length; i++) 
        {
            if (Players[i].Active && Mathf.Abs(Players[i].StepNumber - avgNumber) < difference ) 
            {
                difference = Mathf.Abs(Players[i].StepNumber - avgNumber);
                winner = i;
            }
        }

        RoundWinnerText.text = $"{Players[winner].Name} won round!";
    }

    protected void TakeAwayHP() 
    {
        for(int i = 0; i < Players.Length; i++) 
        {
            if(i != winner) 
                Players[i].HP--;

            Players[i].HPText.text = $"{Players[i].HP}";
        }
    }

    protected void RoundCount() 
    {
        round++;
        RoundText.text = $"Round {round}";
        AvgNumberText.text = null;
        RoundWinnerText.text = null;

        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].StepNumber >= 0)
                Players[i].ValueText.text = $"{Players[i].Name}";
        }
    }

    protected void GameOver() 
    {
        AvgNumberText.text = "";
        RoundWinnerText.text = $"{Players[winner].Name} won the game!";
        RestartButton.gameObject.SetActive(true);
    }
    
    protected int ActivePlayerCount() 
    {
        int activePlayersCount = 0; 

        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].Active)
                activePlayersCount++;
        }

        return activePlayersCount;
    }

    private void SetPlayersAuto()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].HP = 0;
            Players[i].HPText.text = $"{Players[i].HP}";
            Players[i].Active = true;
            Players[i].isBot = true;

            Players[i].RandName();
            Players[i].ValueText.text = $"{Players[i].Name}";
        }
    }

    private void SetIsPlayerBot(bool[] IsPlayerBot) 
    {
        for (int i = 0; i < Players.Length; i++) 
        {
            Players[i].isBot = IsPlayerBot[i];

            if (!Players[i].isBot)
                DataHolder.Users.Add(Players[i]);
        }
    }
}
