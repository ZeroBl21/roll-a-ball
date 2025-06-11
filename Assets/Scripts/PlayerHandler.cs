using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
