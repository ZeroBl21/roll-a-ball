using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody rb;
    private int points = 0;

    public float speed = 10f;
    // public Text pointsText, winText;
    public Text pointsText, winText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = 0;
        setPointsText();
        winText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(
                Input.GetAxis("Horizontal"),
                0.0f,
                Input.GetAxis("Vertical")
                );

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
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
        }
    }
}
