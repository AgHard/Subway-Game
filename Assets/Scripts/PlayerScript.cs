using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public bool isAlive = true;
    public float runSpeed = 1.0f;
    public float horizontalSpeed = 1.0f;
    float horizontalInput;
    public GroundTile groundTile;

    public Rigidbody rb;
    public AudioSource audioSource;
    private float laneWidth = 3f; // Adjust the width of lanes as needed
    private float currentLane = 1; // 0 for left, 1 for center, 2 for right

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    private Renderer rend;
    public AudioClip obstacleHitSound;
    public AudioClip wrongInstruction;
    public AudioClip switchForm;
    public AudioClip usePower;
    public AudioClip obstacleHitSound2;
    public AudioClip gameOverSound;
    public GameObject EndGameMenuUI;
    private Vector2 touchStart;
    private float swipeSensitivity = 50;
    public Text gameOver;
    public GameObject PauseIconUI;
    Coin coin;
    private void Awake()
    {
        rb = GetComponent < Rigidbody>();
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMovement = transform.forward * runSpeed * Time.deltaTime;
            Vector3 horizontalMovement = transform.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement + horizontalMovement);
        }
        else return;
    }
    void Update()
    {
        // Check for lane switching input
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchLane(-1); // Move left
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchLane(1); // Move right
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStart = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float swipeDistance = touch.position.x - touchStart.x;

                    if (Mathf.Abs(swipeDistance) > swipeSensitivity)
                    {
                        // Swipe detected; move the player left or right based on swipe direction
                        if (swipeDistance > 0)
                        {
                            SwitchLane(1);// Right swipe; move player right
                        }
                        else
                        {
                            SwitchLane(-1); // Left swipe; move player left
                        }
                    }
                }
            }
            if (Coin.redState)
            {
                rend.material = material2;
            }
            else if (Coin.greenState)
            {
                rend.material = material4;
            }

            else if (Coin.blueState)
            {
                rend.material = material3;
            }
            else
            {
                rend.material = material1;
            }
            if (Input.GetKeyDown(KeyCode.Space) && Coin.redState)
            {
                if (GameManager.Instance.redscore > 0)
                {
                    Nuke();
                    if (GameManager.Instance.redscore == 0)
                    {
                        Coin.redState = false;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J) && GameManager.Instance.redscore == 5 && !Coin.redState)
            {
                PlayMusic2();
            }
            else if (Input.GetKeyDown(KeyCode.K) && GameManager.Instance.greenscore == 5 && !Coin.greenState)
            {
                PlayMusic2();
            }
            else if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.bluescore == 5 && !Coin.blueState)
            {
                PlayMusic2();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Coin.redState)
            {
                PlayMusic3();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Coin.blueState)
            {
                PlayMusic3();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && Coin.greenState)
            {
                PlayMusic3();
            }
            else if (Input.GetKeyDown(KeyCode.J) && GameManager.Instance.redscore < 5)
            {
                PlayMusic();
            }
            else if (Input.GetKeyDown(KeyCode.K) && GameManager.Instance.greenscore < 5)
            {
                PlayMusic();
            }
            else if (Input.GetKeyDown(KeyCode.L) && GameManager.Instance.bluescore < 5)
            {
                PlayMusic();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.bluescore < 5)
            {
                PlayMusic();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.bluescore < 5)
            {
                PlayMusic();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.bluescore < 5)
            {
                PlayMusic();
            }
        }
        else
        {
            Coin.redState = false;
            Coin.greenState = false;
            Coin.blueState = false;
        }
        // You can add further logic to prevent the player from going out of bounds if needed.
    }

    public void PlayMusic()
    {
        audioSource.PlayOneShot(wrongInstruction);
    }

    public void ActivatePower()
    {
        if (Coin.redState || Coin.greenState || Coin.blueState)
        {
            audioSource.PlayOneShot(usePower);
            if (Coin.redState)
            {
                if (GameManager.Instance.redscore > 0)
                {
                    GameManager.Instance.redscore--;
                    Nuke();
                    if (GameManager.Instance.redscore == 0)
                    {
                        Coin.redState = false;
                    }
                }
            }
            else if (Coin.greenState)
            {
                coin.Multiplier();
            }
            else if (Coin.blueState)
            {
                Coin.DestroyObstacle = true;
            }
        }
        else
        {
            audioSource.PlayOneShot(wrongInstruction);
        }
    }
    public void PlayMusic2()
    {
        audioSource.PlayOneShot(switchForm);
    }

    public void PlayMusic3()
    {
        audioSource.PlayOneShot(usePower);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                audioSource.PlayOneShot(obstacleHitSound);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            if (other.gameObject.CompareTag("Red"))
            {
                audioSource.PlayOneShot(obstacleHitSound2);
            }
            if (other.gameObject.CompareTag("Green"))
            {
                audioSource.PlayOneShot(obstacleHitSound2);
            }
            if (other.gameObject.CompareTag("Blue"))
            {
                audioSource.PlayOneShot(obstacleHitSound2);
            }
        }
    }
    void SwitchLane(int direction)
    {
        // Calculate the new lane position
        int newLane = (int)Mathf.Clamp(currentLane + direction, 0, 2);

        // Calculate the target position based on the lane
        Vector3 targetPosition = transform.position;
        targetPosition.x = (newLane - 1) * laneWidth;

        // Update the current lane and move the player to the new position
        currentLane = newLane;
        rb.MovePosition(targetPosition);
    }

    public void Die()
    {
        isAlive = false;
        audioSource.PlayOneShot(obstacleHitSound);
        StartCoroutine(DestroyObjectAfterDelay(1.0f));
    }

    private IEnumerator DestroyObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
        gameOver.text = "Game Over :- Your Score " + GameManager.Instance.score;
        EndGameMenuUI.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);
        PauseIconUI.SetActive(false);
    }
    public void Nuke()
    {
        groundTile.HideObstaclesFor10Seconds(true);
        groundTile.ObstaclesSpawn(true);
    }
}
