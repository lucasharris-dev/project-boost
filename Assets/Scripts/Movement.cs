using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float forwardThrust = 1000f; // boostSpeed
    [SerializeField] float rotationThrust = 150f; // rotationSpeed

    [SerializeField] AudioClip engineSound;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    // putting references to components on the SAME object as this script in awake
       // is fine, and maybe better, because the references will be assigned as soon
       // as the parent object is enabled (in this case as soon as the scene loads),
       // so any lag will occur at the beginning, and could be hidden as load time
    void Awake()
    {
        // component references could also be in start
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
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
        // Refactored using Extract (CTRL+.)
        // Also, you can rename all instanced using CTRL+F2
        if (boosting)
        {
            BeginThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        bool rotatingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rotatingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (rotatingLeft)
        {
            RotateLeft();
        }
        else if (rotatingRight)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void BeginThrusting()
    {
        myRigidbody.AddRelativeForce(Vector3.up * forwardThrust * Time.deltaTime); // same as new Vector3(0f, forwardThrust, 0f)

        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(engineSound);
        }

        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        myAudioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    //  could also do myRigidbody.AddRelativeTorque, but it's more difficult to control
    void ApplyRotation(float currentRotation)
    {
        myRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * currentRotation * Time.deltaTime);// same as new Vector3 (0f, 0f, 1f);
        myRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
