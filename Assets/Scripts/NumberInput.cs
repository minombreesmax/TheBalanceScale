using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    [SerializeField] Text NumberText;
    [SerializeField] Button OkButton;
    [SerializeField] Button[] NumberButtons;

    private string number;
    private int numChars;

    private void Start()
    {
        StartCoroutine(SetNumber());
    }

    public void ButtonN(Text num) 
    {
        number += num.text;
        numChars++;
        NumberText.text = number;
    }

    public void Clear() 
    {
        number = null;
        numChars = 0;
        NumberText.text = "";
    }

    public void Ok() 
    {
        DataHolder.playerStep = Convert.ToInt32(number);
        DataHolder.numberInputed = true;
        numChars = 0;
        number = "";
        NumberText.text = "";
    }

    private void NumberButtonsStatus(bool interactable)
    {
        for (int i = 0; i < NumberButtons.Length; i++)
        {
            NumberButtons[i].interactable = interactable;
        }

        if (number == "10")
        {
            NumberButtons[0].interactable = true;
        }
    }

    private IEnumerator SetNumber() 
    {
        while (true) 
        {
            OkButton.interactable = number != null ? true : false;
            NumberButtonsStatus(numChars < 2 && number != "0"? true : false);
            yield return null;
        }
    }
}
