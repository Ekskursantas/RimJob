using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Player playerInfo;
    private RimSpawner rimInfo;
    public static bool isNew = true;
    private void Start()
    {
        playerInfo = new Player();
        rimInfo =  new RimSpawner();
    }

    // Start is called before the first frame update
    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isNew = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void loadSave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isNew = false;
    }
}
