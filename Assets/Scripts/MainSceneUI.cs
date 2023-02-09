using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] GameObject GameFields, UIButtons, PauseMenu;

    public void BackToMainMenu()
    {
        GlobalEventManager.ReturnNames();
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause(bool state) 
    {
        GameFields.SetActive(!state);
        UIButtons.SetActive(!state);
        PauseMenu.SetActive(state);
        Time.timeScale = state ? 0f : 1f;
    }

    public void Resume()
    {
        Pause(false);
    }

}
