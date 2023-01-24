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
    public bool isBot { get; set; }
    public string Name { get; set; }

    public void RandName() 
    {
        var rand = Random.Range(0, DataHolder.Names.Count);
        Name = DataHolder.Names[rand];
        DataHolder.Names.RemoveAt(rand);
    }
}



