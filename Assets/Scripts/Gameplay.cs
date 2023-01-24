using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gameplay : Game
{
    void Start()
    {
        GameStart();
        StartCoroutine(Play());
        StartCoroutine(PlayerGameOver());
    }

    private IEnumerator PlayerGameOver()
    {
        while (true)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].HP <= -10)
                {
                    Players[i].HPText.gameObject.SetActive(false);
                    Players[i].ValueText.text = $"{Players[i].Name}\nis out";
                    Players[i].Active = false;

                    if (!DataHolder.Names.Contains(Players[i].Name))
                        DataHolder.Names.Add(Players[i].Name);
                }
            }

            yield return null;
        }
    }

    private IEnumerator UserStep(int i)
    {
        NumberInput.SetActive(true);
        DataHolder.numberInputed = false;
        DataHolder.playerStep = -1;

        while (true)
        {
            if (DataHolder.numberInputed)
            {
                Players[i].StepNumber = DataHolder.playerStep;
                Players[i].ValueText.text = $"{Players[i].Name}\n{Players[i].StepNumber}";
                DataHolder.numberInputed = false;
                break;
            }

            yield return null;
        }

        NumberInput.SetActive(false);
    }

    private IEnumerator Play()
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
                    for (int i = 0; i < DataHolder.Users.Count; i++)
                    {
                        StartCoroutine(UserStep(i));
                        yield return new WaitUntil(() => DataHolder.playerStep >= 0);
                    }
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
