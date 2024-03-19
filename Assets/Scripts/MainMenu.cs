using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject mainMenu;

    public AudioSource menuSound;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        menuSound = GetComponent<AudioSource>();
        menuSound.Play();
    }
    public void onOptionsButton()
    {
        options.SetActive(true);
        mainMenu.SetActive(true);
    }
    public void onPlayButton()
    {
        menuSound.Pause();
        SceneManager.LoadScene("PowerRanger");
    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
