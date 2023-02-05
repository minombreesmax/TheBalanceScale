using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] GameObject StartPlay, GameModes, MultiplayerButtons;
    [SerializeField] Button SinglePlayerButton, LocalMultiPlayerButton;
    private int menuLayer = 0;

    private void Start()
    {
        StartPlay.SetActive(true);
        GameModes.SetActive(false);
    }

    public void Play()
    {
        StartPlay.SetActive(false);
        GameModes.SetActive(true);
        menuLayer++;
    }

    public void SinglePlayer() 
    {
        StartTheGame(1, false, true, true, true, true);
    }

    public void LocalMultiPlayer() 
    {
        SinglePlayerButton.gameObject.SetActive(false);
        LocalMultiPlayerButton.gameObject.SetActive(false);
        MultiplayerButtons.SetActive(true);
        menuLayer++;
    }

    #region Set n-Players and k-AI
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
    #endregion

    public void BackTo() 
    {
        if (menuLayer == 2) 
        {
            BackToGameModes();
        }
        else 
        {
            BackToStartMenu();
        }

        menuLayer--;
    }

    public void Exit() 
    {
        Application.Quit();
    }

    private void BackToGameModes() 
    {
        MultiplayerButtons.SetActive(false);
        SinglePlayerButton.gameObject.SetActive(true);
        LocalMultiPlayerButton.gameObject.SetActive(true);
    }

    private void BackToStartMenu() 
    {
        GameModes.SetActive(false);
        StartPlay.SetActive(true);
    }

    private void StartTheGame(int sceneN, params bool[] player) 
    {
        for(int i = 0; i < player.Length; i++) // player.Length must be equal 5
            DataHolder.IsPlayerBot[i] = player[i];

        SceneManager.LoadScene(sceneN);
    }
}
