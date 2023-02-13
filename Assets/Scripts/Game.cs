using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    [SerializeField] protected Text AvgNumberText, RoundWinnerText, RoundText, NumberInputHeaderText;
    [SerializeField] protected Player[] Players;
    [SerializeField] protected GameObject NumberInput;
    [SerializeField] protected Button RestartButton;
    [SerializeField] protected Animator NumberInputAnimator;
    
    protected List<Player> Users = new List<Player>();
    private Player[] PrevPlayers = new Player[5];

    protected int round = 0, winner;
    protected float avgNumber;

    protected void GameStart() 
    {
        RestartButton.gameObject.SetActive(false);
        SetPlayersAuto();
        SetIsPlayerBot(DataHolder.IsPlayerBot);
    }

    protected void SetNumbers() 
    {
        for (int i = 0; i < Players.Length; i++) 
        {
            PrevPlayers[i] = Players[i];

            if (Players[i].IsBot)
            {
                Players[i].StepNumber = Players[i].StepNumber == 0 ? 
                    UnityEngine.Random.Range(0, 101) : Players[i].SmartDigitSelection(PrevPlayers, Players[i].Name);
            }

            Players[i].ValueText.text = $"{Players[i].Name}\n{Players[i].StepNumber}";
        }

        NumberInputAnimator.Play("NumberInputOut");
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
        AvgNumberText.text = $"{Math.Round(avgNumber, 2)}";
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
        GlobalEventManager.ConvertTextToSpeach();
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

        NumberInputAnimator.Play("NumberInputComing");
    }

    protected void GameOver() 
    {
        AvgNumberText.text = "";
        RoundWinnerText.text = $"{Players[winner].Name} won the game!";
        Players[winner].ValueText.text = $"{Players[winner].Name}\n Winner!";
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

    protected void AddToNames(Player player) 
    {
        if (!DataHolder.Names.Contains(player.Name))
        {
            DataHolder.Names.Add(player.Name);
        }
    }

    private void SetPlayersAuto()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].HP = 0;
            Players[i].HPText.text = $"{Players[i].HP}";
            Players[i].Active = true;
            Players[i].IsBot = true;

            Players[i].RandName();
            Players[i].ValueText.text = $"{Players[i].Name}";
        }
    }

    private void SetIsPlayerBot(bool[] IsPlayerBot) 
    {
        for (int i = 0; i < Players.Length; i++) 
        {
            Players[i].IsBot = IsPlayerBot[i];

            if (!Players[i].IsBot) 
                Users.Add(Players[i]);
        }
    }


}
