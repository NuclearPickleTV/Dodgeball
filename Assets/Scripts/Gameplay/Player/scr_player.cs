using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_player : MonoBehaviour
{
    public Canvas uiCanvas;
    public GameObject highScoreText;
    public GameObject livesText;
    public GameObject playButton;
    public GameObject allTimeScoreText;
    public Rigidbody playerRB;
    public GameObject blurPanel;

    public bool isAlive;
    public bool isPaused;

    public float movementSpeed;
    public float jumpForce = 2.0f;

    public int highScore;
    public int allTimeScore;

    int currentLives;
    //bool isGrounded;
    Vector3 jump;
    Rigidbody rb;

    void OnCollisionStay()
    {
        //isGrounded = true;
    }
    void OnCollisionExit()
    {
        //isGrounded = false;
    }

    class SaveData
    {
        public int savedHighScore;
    }

    private void Start()
    {
        pauseGame();

        isAlive = true;

        allTimeScore = 0;
        highScore = 0;
        currentLives = 3;

        LoadGame();

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("All Time Score", allTimeScore);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("All Time Score"))
        {
            allTimeScore = PlayerPrefs.GetInt("All Time Score");
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    void resetData()
    {
        PlayerPrefs.DeleteAll();
        restartGame();
        Debug.Log("Data reset complete");
    }

    public void pauseGame()
    {
        playButton.SetActive(true);
        blurPanel.SetActive(true);
        highScoreText.SetActive(false);
        allTimeScoreText.SetActive(false);
        livesText.SetActive(false);

        Time.timeScale = 0;
        isPaused = true;
    }

    public void resumeGame()
    {
        playButton.SetActive(false);
        blurPanel.SetActive(false);
        highScoreText.SetActive(true);
        allTimeScoreText.SetActive(true);
        livesText.SetActive(true);

        Time.timeScale = 1;
        isPaused = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            isAlive = false;
        }
    }

    void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (isPaused == true)
        {
            pauseGame();
        }

        if (isAlive == false)
        {
            SaveGame();
            restartGame();
        }

        if (highScore >= allTimeScore) { allTimeScore = highScore; }

        allTimeScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "High Score: " + allTimeScore;
        highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + highScore;
        livesText.GetComponent<TMPro.TextMeshProUGUI>().text = "Lives: " + currentLives;


        if (Input.GetKey("w"))
        {
            movementVector += new Vector3(0f, 0f, movementSpeed);
        }

        if (Input.GetKey("s"))
        {
            movementVector += new Vector3(0f, 0f, -movementSpeed);
        }

        if (Input.GetKey("a"))
        {
            movementVector += new Vector3(-movementSpeed, 0f, 0f);
        }

        if (Input.GetKey("d"))
        {
            movementVector += new Vector3(movementSpeed, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            resetData();
        }

        movementVector = Vector3.ClampMagnitude(movementVector, 1f) * movementSpeed;
        playerRB.position += movementVector * Time.deltaTime;

        /*if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }*/
    }
}
