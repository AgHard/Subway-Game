using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject PauseMenuUI;
    public GameObject PauseIconUI;
    public GameObject EndGameMenuUI;
    public PlayerScript playerScript;
    public static bool GameIsStopped = false;
    public AudioSource source;
    int x = 0;
    private void Update()
    {
        if (x == 0)
        {
            source.Stop();
            x = 1;
        }
        if (playerScript.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsStopped)
                {
                    source.Pause();
                    playerScript.audioSource.UnPause();
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    pause();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        
    }
    public void Resume()
    {
        source.Pause();
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuUI.SetActive(false);
        PauseIconUI.SetActive(true);
        GameIsStopped = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("PowerRanger");
        }
        else
        {
            SceneManager.LoadScene("PowerRanger");
        }
        
    }

    public void LoadMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void QuitGame()
    {
        Debug.Log("GG......");
        Application.Quit();
    }

    public void pause()
    {
        playerScript.audioSource.Pause();
        source.Play();
        PauseIconUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        GameIsStopped = true;
        Time.timeScale = 0f;
    }
}
