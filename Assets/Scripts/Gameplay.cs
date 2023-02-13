using System.Collections;
using UnityEngine;

public class Gameplay : Game
{
    void Start()
    {
        GameStart();
        StartCoroutine(Play());
        StartCoroutine(PlayerGameOver());
        GlobalEventManager.ReturnNamesEvent.AddListener(ReturnUsedNames);
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
                    AddToNames(Players[i]);
                }
            }

            yield return null;
        }
    }

    private IEnumerator UserStep(int i)
    {
        DataHolder.numberInputed = false; 
        Users[i].StepNumber = 0;
        NumberInputHeaderText.text = $"{Players[i].Name}";
        GlobalEventManager.StartTimer();

        while (true)
        {
            if (DataHolder.numberInputed)
            {
                GlobalEventManager.StopTimer();
                Players[i].StepNumber = DataHolder.playerStep;
                Users[i].StepNumber = Players[i].StepNumber;
                break;
            }

            yield return null;
        }
    }

    private IEnumerator Play()
    {
        while (true)
        {
            print("play");
            if (ActivePlayerCount() <= 1)
            {
                GameOver();
                break;
            }
            else
            {
                RoundCount();
                yield return new WaitForSeconds(1f);

                for (int i = 0; i < Users.Count; i++)
                {
                    if (Users[i].Active)
                    {
                        StartCoroutine(UserStep(i));
                        yield return new WaitUntil(() => Users[i].StepNumber > 0);
                    }
                }

                SetNumbers();
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

    private void ReturnUsedNames() 
    {
        foreach(var player in Players) 
        {
            AddToNames(player);
        }
    }
}
