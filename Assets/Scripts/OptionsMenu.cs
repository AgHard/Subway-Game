using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject mainMenu;
    public void onOptionsButton()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }
}
