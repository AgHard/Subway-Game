using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Coin : MonoBehaviour
{
    public static bool redState = false;
    public static bool redState2 = false;
    public static bool blueState = false;
    public static bool greenState = false;
    public static bool DestroyObstacle = false;
    public static bool multi = false;
    public static bool shield = false;
    public static bool multiple = false;
    public static bool JMusic = false;
    public static bool KMusic = false;
    
    
    public static bool LMusic = false;
    GroundTilee groundTilee;
    // Start is called before the first frame update
    void Start()
    {
        groundTilee = GameObject.FindAnyObjectByType<GroundTilee>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HitObstacles>() != null)
        {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.name != "Player")
        {
            return;
        }
        GameManager.Instance.IncScore();

        if (gameObject.CompareTag("Red"))
        {
            GameManager.Instance.IncRedScore();
        }
        else if (gameObject.CompareTag("Green"))
        {
            GameManager.Instance.IncGreenScore();
        }
        else if (gameObject.CompareTag("Blue"))
        {
            GameManager.Instance.IncBlueScore();
        }
        Destroy(gameObject);
        if (GameManager.Instance.greenscore == 0)
        {
            greenState = false;
        }
        if (GameManager.Instance.redscore == 0)
        {
            redState = false;
        }
        if (GameManager.Instance.bluescore == 0)
        {
            blueState = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && GameManager.Instance.redscore == 5 && !redState)
        {
            GameManager.Instance.redscore--;
            redState = true;
            blueState = false;
            greenState = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.K) && GameManager.Instance.greenscore == 5 && !greenState)
        {
            GameManager.Instance.greenscore--;
            greenState = true;
            redState = false;
            blueState = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.bluescore == 5 && !blueState)
        {
            GameManager.Instance.bluescore--;
            blueState = true;
            greenState = false;
            redState = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.Space) && greenState)
        {
            Multiplier();
        }
    }

    public void JPower()
    {
        if (GameManager.Instance.redscore == 5 && !redState)
        {
            
            GameManager.Instance.redscore--;
            redState = true;
            blueState = false;
            greenState = false;
            //groundTilee.colors.PlayOneShot(groundTilee.s);
        }
    }

    public void KPower()
    {
        if (GameManager.Instance.greenscore == 5 && !greenState)
        {
            

            GameManager.Instance.greenscore--;
            redState = false;
            blueState = false;
            greenState = true;
        }
    }

    public void LPower()
    {
        if (GameManager.Instance.bluescore == 5 && !blueState)
        {
            Debug.Log("ss");

           // aa.Play();
            GameManager.Instance.bluescore--;
            redState = false;
            blueState = true;
            greenState = false;
        }
    }
    public void Multiplier()
    {
        
        if (GameManager.Instance.greenscore == 0)
        {
            multi = false;
        }
        else if (greenState)
        {
            multi = true;
            multiple = true;
        }
        
    }
}
