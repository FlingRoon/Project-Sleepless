using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUi : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Night 1";

    public GameObject loadMenu;
    public GameObject settingMenu;

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
    }

    public void LoadGameButton()
    {
        settingMenu.SetActive(false);
        loadMenu.SetActive(true);
    }
    public void settingsButton()
    {
        loadMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void loadSave1()
    {

    }
    public void loadSave2()
    {

    }
    public void loadSave3()
    {

    }


    public void altF4Button()
    {
        doExitGame();
    }
    void doExitGame()
    {
        Application.Quit();
        Debug.Log("Game will Alt + F4");
    }
    public void backToMainMenuButton()
    {
        SceneManager.LoadScene("StartingScreen");
    }

}
