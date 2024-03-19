using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacles : MonoBehaviour
{
    PlayerScript player;
    Coin coin;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && !Coin.DestroyObstacle && !Coin.redState && !Coin.blueState && !Coin.greenState)
        {
            player.Die();
        }
        else if (collision.gameObject.name == "Player" && Coin.DestroyObstacle && Coin.blueState)
        {
            Destroy(gameObject);
            Coin.DestroyObstacle = false;
            Coin.shield = false;
            if (GameManager.Instance.bluescore < 1 )
            {
                Coin.blueState = false;
            }
        }
        else if (collision.gameObject.name == "Player" && Coin.greenState)
        {
            Destroy(gameObject);
            Coin.greenState = false;
        }
        else if (collision.gameObject.name == "Player" && Coin.redState)
        {
            Destroy(gameObject);
            Coin.redState = false;
        }
        else if (collision.gameObject.name == "Player" && Coin.blueState)
        {
            Destroy(gameObject);
            Coin.blueState = false;
        }
    }
}
