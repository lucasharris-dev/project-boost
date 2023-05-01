using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float boostSpeed = 1000f;
    [SerializeField] float rotationThrust = 150f; // rotationSpeed

    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // GetKey is true when a key is held
    // GetKeyDown is true on the first frame that a key is pressed
    // GetKeyUp is true on the first frame that a key is released
    void ProcessThrust()
    {
        bool boosting = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);
        if (boosting)
        {
            myRigidbody.AddRelativeForce(Vector3.up * boostSpeed * Time.deltaTime); // same as new Vector3(0f, boostSpeed, 0f)
            Debug.Log("Boosting");
        }
    }

    void ProcessRotation()
    {
        bool rotatingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rotatingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (rotatingLeft)
        {
            ApplyRotation(rotationThrust);
            // Debug.Log("Rotating left");
        }
        else if (rotatingRight)
        {
            ApplyRotation(-rotationThrust);
            // Debug.Log("Rotating right");
        }
    }

    void ApplyRotation(float rotationDirection)
    {
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);// same as new Vector3 (0f, 0f, 1f)
        //myRigidbody.AddRelativeTorque(Vector3.forward * rotationDirection * Time.deltaTime); // more difficult to control
    }
}
