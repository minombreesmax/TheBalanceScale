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
    [SerializeField] private Text AvgNumberText, RoundWinnerText, RoundText;
    [SerializeField] private Player[] Players;
    [SerializeField] private GameObject NumberInput;
    [SerializeField] private Button RestartButton;

    private int round = 0, winner;
    private float avgNumber;

    void Start()
    {
        GameStart();
        StartCoroutine(Gameplay());
        StartCoroutine(PlayerGameOver());
    }

    private void GameStart() 
    {
        RestartButton.gameObject.SetActive(false);

        for(int i = 0; i < Players.Length; i++) 
        {
            Players[i].HP = 0;
            Players[i].HPText.text = $"{Players[i].HP}";
            Players[i].Active = true;
            Players[i].isBot = true;
        }

        Players[0].Name = "You";
        Players[0].isBot = false;
    }
    
    private void RandNumeric() 
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

    private void CountAvgNumber() 
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

    private void GetRoundWinner() 
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

        if (ActivePlayerCount() < 5)
            AddFirstNewRule();

        if (ActivePlayerCount() < 4)
            AddSecondNewRule();

        if (ActivePlayerCount() < 3)
            AddThirdNewRule();

        RoundWinnerText.text = $"{Players[winner].Name} won round!";
    }

    private void AddFirstNewRule() 
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

    private void AddSecondNewRule() 
    {
        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].StepNumber == Math.Round(avgNumber, 0)) 
            {
                winner = i;
                TakeAwayHP();
            }
        }

        print("Second rule has been activated");
    }

    private void AddThirdNewRule() 
    {
        for(int i = 0; i < Players.Length; i++) 
        {
            for (int j = 0; j < Players.Length; j++)
            {
                if (Players[i].Active && Players[j].Active && i != j) 
                {
                    if (Players[i].StepNumber == 0 && Players[j].StepNumber == 100) 
                    {
                        winner = j;
                    }
                    else if(Players[i].StepNumber == 100 && Players[j].StepNumber == 0) 
                    {
                        winner = i;
                    }
                }     
            }
        }

        print("Third rule has been activated");
    }

    private void TakeAwayHP() 
    {
        for(int i = 0; i < Players.Length; i++) 
        {
            if(i != winner) 
                Players[i].HP--;

            Players[i].HPText.text = $"{Players[i].HP}";
        }
    }

    private void RoundCount() 
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

    private int ActivePlayerCount() 
    {
        int activePlayersCount = 0; 

        for(int i = 0; i < Players.Length; i++) 
        {
            if (Players[i].Active)
                activePlayersCount++;
        }

        return activePlayersCount;
    }

    private void GameOver() 
    {
        AvgNumberText.text = "";
        RoundWinnerText.text = $"{Players[winner].Name} won the game!";
        RestartButton.gameObject.SetActive(true);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator PlayerGameOver() 
    {
        while (true)
        {
            for(int i = 0; i < Players.Length; i++) 
            {
                if (Players[i].HP <= -10) 
                {
                    Players[i].HPText.gameObject.SetActive(false);
                    Players[i].ValueText.text = $"{Players[i].Name}\nis out";
                    Players[i].Active = false;
                }
            }

            yield return null;
        }
    }

    private IEnumerator UserStep()
    {
        NumberInput.SetActive(true);
        DataHolder.numberInputed = false;
        DataHolder.playerStep = -1;

        while (true)
        {
            if (DataHolder.numberInputed)
            {
                Players[0].StepNumber = DataHolder.playerStep;
                Players[0].ValueText.text = $"{Players[0].Name}\n{Players[0].StepNumber}";
                DataHolder.numberInputed = false;
                break;
            }

            yield return null;
        }

        NumberInput.SetActive(false);
    }

    private IEnumerator Gameplay() 
    {
        while (true)
        {
            if (ActivePlayerCount() == 1)
            {
                GameOver();
                break;
            }
            else
            {
                RoundCount();
                yield return new WaitForSeconds(1f);

                if (Players[0].Active)
                {
                    StartCoroutine(UserStep());
                    yield return new WaitUntil(() => DataHolder.playerStep >= 0);
                }

                RandNumeric();
                yield return new WaitForSeconds(2f);

                CountAvgNumber();
                yield return new WaitForSeconds(2f);

                GetRoundWinner();
                yield return new WaitForSeconds(1f);

                TakeAwayHP();
            }

            yield return new WaitForSeconds(2f);
        }
    }
    
   
}
