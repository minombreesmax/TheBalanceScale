using System.Collections;
using UnityEngine;

public class Gameplay : Game
{
    [SerializeField] Animator NumberInputAnimator;

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
                    {
                        DataHolder.Names.Add(Players[i].Name);
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator UserStep(int i)
    {
        NumberInputAnimator.Play("NumberInputComing");
        DataHolder.numberInputed = false;
        DataHolder.Users[i].StepNumber = 0;

        while (true)
        {
            if (DataHolder.numberInputed)
            {
                Players[i].StepNumber = DataHolder.playerStep;
                DataHolder.Users[i].StepNumber = Players[i].StepNumber;
                DataHolder.numberInputed = false;
                break;
            }

            yield return null;
        }
    }

    private IEnumerator Play()
    {
        while (true)
        {
            if (ActivePlayerCount() <= 1)
            {
                GameOver();
                break;
            }
            else
            {
                RoundCount();
                yield return new WaitForSeconds(1f);

                for (int i = 0; i < DataHolder.Users.Count; i++)
                {
                    if (DataHolder.Users[i].Active)
                    {
                        StartCoroutine(UserStep(i));
                        yield return new WaitUntil(() => DataHolder.Users[i].StepNumber > 0);
                    }
                }

                NumberInputAnimator.Play("NumberInputOut");

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
}
