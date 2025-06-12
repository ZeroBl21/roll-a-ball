using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody rb;
    private int points = 0;

    public float speed = 10f;
    public Text pointsText, winText, timerText;

    public float timerDuration = 120f; // duración total en segundos
    private float timeRemaining;
    private bool gameEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = 0;
        timeRemaining = timerDuration;
        setPointsText();
        winText.text = "";
        UpdateTimerText();
    }

    void FixedUpdate()
    {
        if (gameEnded) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(inputVector * speed, ForceMode.Force);

        Vector3 torque = new Vector3(inputVector.z, 0, -inputVector.x);
        rb.AddTorque(torque * speed * 0.5f);
    }

    void Update()
    {
        if (gameEnded) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UpdateTimerText();

        if (timeRemaining <= 0 && winText.text != "You Win!!")
        {
            winText.text = "You Lose";
            gameEnded = true;
            Invoke("RestartLevel", 2f);
        }
    }

    void LateUpdate()
    {
        float maxSpeed = 30f;
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            points++;
            setPointsText();
        }
    }

    void setPointsText()
    {
        pointsText.text = "Points: " + points.ToString();
        if (points >= 12)
        {
            winText.text = "You Win!!";
            gameEnded = true;
            Invoke("LoadNextLevel", 2f);
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡Has terminado todos los niveles!");
            SceneManager.LoadScene("Main Menu");
        }
    }
}
