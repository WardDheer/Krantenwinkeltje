using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        GameIsPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
