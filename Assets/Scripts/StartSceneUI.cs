using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] GameObject StartPlay, GameModes, ExitMenu, SettingsMenu;
    [SerializeField] Button StartPlayButton, ExitButton, SettingsButton;
    [SerializeField] Text StartPlayText;

    public void Play() 
    {
        StartPlay.gameObject.SetActive(false);
        GameModes.gameObject.SetActive(true);
    }

    public void ExitMenuOn()
    {
        SetExitMenu(true);
    }

    public void SettingsMenuOn() 
    {
        SetSettingsMenu(true);
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
        ExitMenu.gameObject.SetActive(state);
        StartPlayButton.interactable = !state;
        StartPlayText.gameObject.SetActive(!state);
        ExitButton.gameObject.SetActive(!state);
        SettingsButton.gameObject.SetActive(!state);
    }

    private void SetSettingsMenu(bool state) 
    {
        StartPlay.gameObject.SetActive(!state);
        SettingsMenu.gameObject.SetActive(state);
    }

}
