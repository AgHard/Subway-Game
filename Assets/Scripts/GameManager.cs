using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public int redscore;
    public int bluescore;
    public int greenscore;
    public static GameManager Instance;
    Coin coin;
    public Text scoreText;
    public Text redscoreText;
    public Text bluescoreText;
    public Text greenscoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public void IncScore()
    {
        score++;
    }
    public void IncRedScore()
    {
        if (Coin.redState)
        {
            IncScore();
        }
        else if (Coin.greenState && Coin.multi)
        {
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            Coin.multiple = false;
            if (redscore < 4)
            {
                redscore += 2;
            }
            else if (redscore < 5)
            {
                redscore++;
            }
            Coin.multi = false;
        }
        else if (redscore < 5 && !Coin.redState)
        {
            redscore++;
        }
        else if (Coin.greenState)
        {
            if (redscore < 5)
            {
                redscore++;
            }
        }
    }
    public void IncBlueScore()
    {
        if (bluescore == 5)
        {
            bluescore = 5;
        }
        if (Coin.blueState)
        {
            IncScore();
        }
        else if (Coin.greenState && Coin.multi)
        {
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            Coin.multiple = false;
            Coin.multi = false;
        }
        else if (bluescore < 5 && !Coin.blueState)
        {
            bluescore++;
        }
        else if (Coin.greenState)
        {
            if (bluescore < 5)
            {
                bluescore++;
            }  
        }
        if (bluescore < 5 && Coin.greenState && Coin.multi)
        {
            bluescore++;
            if (bluescore < 5)
            {
                bluescore++;
            }
        }
    }
    public void IncGreenScore()
    {
        if (Coin.greenState && Coin.multi)
        {
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            IncScore();
            Coin.multiple = false;
            Coin.multi = false;
        }
        else if (greenscore < 5 && !Coin.greenState)
        {
            greenscore++;
        }
        else if (Coin.greenState)
        {
            IncScore();
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Coin.greenState && !Coin.multiple)
        {
            greenscore--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Coin.blueState && !Coin.shield)
        {
            bluescore--;
            Coin.shield = true;
            Coin.DestroyObstacle = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Coin.redState && redscore > 0)
        {
            redscore--;
        }
        else if (Coin.redState && redscore == 0)
        {
            Coin.redState = false;
        }
        scoreText.text = "Score : " + score;
        redscoreText.text = "Red Score : " + redscore;
        bluescoreText.text = "Blue Score : " + bluescore;
        greenscoreText.text = "Green Score : " + greenscore;
    }
}
