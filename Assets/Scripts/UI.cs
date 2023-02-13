using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject GameFields, UIButtons, PauseMenu;

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
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
<<<<<<< HEAD:Assets/Scripts/MainSceneUI.cs

=======
>>>>>>> parent of 3b49ed2 (Added new sounds, features and base for future updates):Assets/Scripts/UI.cs
}
