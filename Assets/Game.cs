using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Game : MonoBehaviour
{
    [SerializeField] private Text[] PlayersValuesText, PlayersHPText;
    [SerializeField] private Text AvgNumberText, RoundWinnerText, RoundText;

    private int[] PlayerHP, PlayerStepNumber;
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
        PlayerHP = new int[PlayersValuesText.Length];
        PlayerStepNumber = new int[PlayersValuesText.Length];

        for(int i = 0; i < PlayerHP.Length; i++) 
        {
            PlayerHP[i] = 0;
            PlayersHPText[i].text = $"{PlayerHP[i]}";
        }
    }

    private void RandNumeric() 
    {
        for(int i = 0; i < PlayerStepNumber.Length; i++) 
        {
            PlayerStepNumber[i] = Random.Range(0, 100);
            PlayersValuesText[i].text = $"Player {i + 1}:\n{PlayerStepNumber[i]}";
        }
    }

    private void CountAvgNumber() 
    {
        avgNumber = 0;

        for(int i = 0; i < PlayerStepNumber.Length; i++) 
        {
            if(PlayerStepNumber[i] >= 0)
                avgNumber += PlayerStepNumber[i];
        }

        avgNumber = (avgNumber / ActivePlayerCount()) * 0.8f;
        AvgNumberText.text = $"{avgNumber}";
    }

    private void GetRoundWinner() 
    {
        winner = 0;
        var difference = Mathf.Abs(PlayerStepNumber[0] - avgNumber);

        for (int i = 1; i < PlayerStepNumber.Length; i++) 
        {
            if(Mathf.Abs(PlayerStepNumber[i] - avgNumber) < difference) 
            {
                difference = Mathf.Abs(PlayerStepNumber[i] - avgNumber);
                winner = i;
            }
        }

        RoundWinnerText.text = $"Player {winner + 1} won!";
    }

    private void TakeAwayHP() 
    {
        for(int i = 0; i < PlayerHP.Length; i++) 
        {
            if(i != winner) 
            {
                PlayerHP[i]--;
            }

            PlayersHPText[i].text = $"{PlayerHP[i]}";
        }
    }

    private void RoundCount() 
    {
        round++;
        RoundText.text = $"Round {round}";
        AvgNumberText.text = null;
        RoundWinnerText.text = null;

        for(int i = 0; i < PlayersValuesText.Length; i++) 
        {
            if(PlayerStepNumber[i] >= 0)
                PlayersValuesText[i].text = $"Player {i + 1}:";
        }
    }

    private int ActivePlayerCount() 
    {
        int activePlayersCount = 0; 

        for(int i = 0; i < PlayerStepNumber.Length; i++) 
        {
            if(PlayerStepNumber[i] >= 0) 
            {
                activePlayersCount++;
            }
        }

        return activePlayersCount;
    }

    private void GameOver() 
    {
        RoundWinnerText.text = $"Player {winner + 1} won the game!";
    }

    private IEnumerator PlayerGameOver() 
    {
        while (true)
        {
            for(int i = 0; i < PlayerHP.Length; i++) 
            {
                if(PlayerHP[i] <= -10) 
                {
                    PlayersHPText[i].gameObject.SetActive(false);
                    PlayersValuesText[i].text = $"Player {i}\nis out";
                    PlayerStepNumber[i] = -1;
                }
            }

            yield return null;
        }
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
