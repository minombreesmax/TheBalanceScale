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

    private List<string> Names = new List<string>() { 
        "Sofia", "Ann", "Maria", "Arisu", "Emma", "Ivan", 
        "Alex", "Jack", "Andrew", "Mark", "Li", "Minato"};

    private void Start()
    {
        RandName();
    }

    private void RandName() 
    {
        var rand = Random.Range(0, Names.Count);
        Name = Names[rand];
    }
}



