using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float forwardThrust = 1000f; // boostSpeed
    [SerializeField] float rotationThrust = 150f; // rotationSpeed

    Rigidbody myRigidbody;

    // putting references to components on the SAME object as this script in awake
       // is fine, and maybe better, because the references will be assigned as soon
       // as the parent object is enabled (in this case as soon as the scene loads),
       // so any lag will occur at the beginning, and could be hidden as load time
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>(); // could also be in start
    }

    void Start()
    {
        
    }

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
            myRigidbody.AddRelativeForce(Vector3.up * forwardThrust * Time.deltaTime); // same as new Vector3(0f, forwardThrust, 0f)
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

    //  could also do myRigidbody.AddRelativeTorque, but it's more difficult to control
    void ApplyRotation(float currentRotation)
    {
        myRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * currentRotation * Time.deltaTime);// same as new Vector3 (0f, 0f, 1f);
        myRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
