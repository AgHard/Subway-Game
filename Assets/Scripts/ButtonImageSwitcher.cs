using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    public Button button; // Reference to your button
    public Sprite image1; // The first image
    public Sprite image2; // The second image

    private Image buttonImage;
    private bool isImage2State = false;

    private void Start()
    {
        buttonImage = button.GetComponent < Image>();
        button.onClick.AddListener(ToggleImage);

        // Load the button state from PlayerPrefs
        isImage2State = PlayerPrefs.GetInt("ButtonState", 0) == 1;
        UpdateButtonImage();
    }

    private void ToggleImage()
    {
        isImage2State = !isImage2State;
        // Save the button state to PlayerPrefs
        PlayerPrefs.SetInt("ButtonState", isImage2State ? 1 : 0);
        PlayerPrefs.Save();
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        buttonImage.sprite = isImage2State ? image2 : image1;
        // Update audio mute state based on the button state
        AudioListener.pause = isImage2State;
    }
}
