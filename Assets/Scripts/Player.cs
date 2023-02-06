using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text ValueText, HPText;
    public float HP { get; set; }
    public int StepNumber { get; set; }
    public bool Active { get; set; }
    public bool IsBot { get; set; }
    public string Name { get; set; }

    public void RandName() 
    {
        var rand = Random.Range(0, DataHolder.Names.Count);
        Name = DataHolder.Names[rand];
        DataHolder.Names.RemoveAt(rand);
    }

    public int SmartDigitSelection(Player[] players, string name) 
    {
        int digit = 0;

        foreach (var player in players) 
        {
            if (player.Name != name)
                digit += player.StepNumber;
        }

        digit /= 4;
        digit += Random.Range(-20, 20);

        return Mathf.Clamp(digit, 0, 100); 
    }

}



