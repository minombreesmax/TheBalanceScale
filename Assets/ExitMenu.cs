using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] public Button StartPlayButton, ExitButton, SettingsButton;
    [SerializeField] public Text StartPlayText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ExitMenuOn()
    {
        SetExitMenu(true);
    }

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        SetExitMenu(false);
    }

    private void SetExitMenu(bool state)
    {
        gameObject.SetActive(state);
        StartPlayButton.interactable = !state;
        StartPlayText.gameObject.SetActive(!state);
        ExitButton.gameObject.SetActive(!state);
        SettingsButton.gameObject.SetActive(!state);
    }

}
