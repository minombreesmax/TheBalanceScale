using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text TimerText;
    private int seconds;
    private string zero;

    private void Start()
    {
        GlobalEventManager.StartTimerEvent.AddListener(TimerStart);
        GlobalEventManager.StopTimerEvent.AddListener(TimerStop);
    }

    public void TimerStart() 
    {
        TimerText = GetComponent<Text>();
        TimerText.gameObject.SetActive(true);
        seconds = 15;
        StartCoroutine(TimerWork());
    }

    public void TimerStop() 
    {
        TimerText.gameObject.SetActive(false);
        StopAllCoroutines();
    }
    
    private IEnumerator TimerWork() 
    {
        while (true)
        {
            zero = seconds > 9 ? "" : "0";
            TimerText.text = $"0:{zero}{seconds}";

            if (seconds < 0)
            {
                AutoPlayerStep();
                break;
            }

            yield return new WaitForSeconds(1);
            seconds--;
        }
    }

    private void AutoPlayerStep() 
    {
        TimerText.gameObject.SetActive(false);
        DataHolder.numberInputed = true;
        DataHolder.playerStep = 100;
    }
    
}
