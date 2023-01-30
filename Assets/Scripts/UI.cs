using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
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
        StartTheGame(false, true, true, true, true, 1);
    }

    public void LocalMultiPlayer() 
    {
        SinglePlayerButton.gameObject.SetActive(false);
        LocalMultiPlayerButton.gameObject.SetActive(false);
        MultiplayerButtons.SetActive(true);
        menuLayer++;
    }

    public void Set2Players3AI() 
    {
        StartTheGame(false, false, true, true, true, 2);
    }

    public void Set3Players2AI()
    {
        StartTheGame(false, false, false, true, true, 2);
    }

    public void Set4Players1AI()
    {
        StartTheGame(false, false, false, false, true, 2);
    }

    public void Set5Players()
    {
        StartTheGame(false, false, false, false, false, 2);
    }

    public void Back() 
    {
        if(menuLayer == 2) 
        {
            BackToGameModes();
        }
        else 
        {
            BackToStertMenu();
        }

        menuLayer--;
    }

    private void BackToGameModes() 
    {
        MultiplayerButtons.SetActive(false);
        SinglePlayerButton.gameObject.SetActive(true);
        LocalMultiPlayerButton.gameObject.SetActive(true);
    }

    private void BackToStertMenu() 
    {
        GameModes.SetActive(false);
        StartPlay.SetActive(true);
    }

    private void StartTheGame(bool player1, bool player2, bool player3, bool player4, bool player5, int sceneN) 
    {
        DataHolder.IsPlayerBot[0] = player1;
        DataHolder.IsPlayerBot[1] = player2;
        DataHolder.IsPlayerBot[2] = player3;
        DataHolder.IsPlayerBot[3] = player4;
        DataHolder.IsPlayerBot[4] = player5;
        SceneManager.LoadScene(sceneN);
    }
}
