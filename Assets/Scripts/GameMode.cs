using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] Button SinglePlayerButton, LocalMultiPlayerButton, BackToMainMenuButton;
    [SerializeField] GameObject GameModes, StartPlay, MultiplayerButtons;

    public void SinglePlayer() 
    {
        StartTheGame(1, false, true, true, true, true);
    }

    public void LocalMultiplayer() 
    {
        LocalMultiplayerOn(true);
    }

    public void Set2Players3AI() 
    {
        StartTheGame(1, false, false, true, true, true);
    }

    public void Set3Players2AI()
    {
        StartTheGame(1, false, false, false, true, true);
    }

    public void Set4Players1AI()
    {
        StartTheGame(1, false, false, false, false, true);
    }

    public void Set5Players()
    {
        StartTheGame(1, false, false, false, false, false);
    }

    public void BackToMainMenu() 
    {
        GameModes.SetActive(false);
        StartPlay.SetActive(true);
    }

    public void BackToModeSelect() 
    {
        LocalMultiplayerOn(false);
    }

    private void LocalMultiplayerOn(bool state)
    {
        SinglePlayerButton.gameObject.SetActive(!state);
        LocalMultiPlayerButton.gameObject.SetActive(!state);
        BackToMainMenuButton.gameObject.SetActive(!state);
        MultiplayerButtons.SetActive(state);
    }

    private void StartTheGame(int sceneN, params bool[] player) 
    {
        for(int i = 0; i < player.Length; i++) // player.Length must be equal 5
            DataHolder.IsPlayerBot[i] = player[i];

        SceneManager.LoadScene(sceneN);
    }
}
