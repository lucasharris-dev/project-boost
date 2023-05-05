using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector = new Vector3(0f, -15f, 0f);
    [SerializeField] float period = 2f;

    float movementFactor;

    void Awake()
    {
        startingPosition = transform.position;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveParent();
    }

    void MoveParent()
    {
        if (period <= Mathf.Epsilon) // shouldn't use == with floats, because it is HIGHLY unlikely to be exactly the same, so == 0f is bad
        {
            return;
        }

        const float tau = Mathf.PI * 2; // tau is 2pi (6.283...), aka the full length around a circle in radians
        float cycles = Time.time / period; // this will count the number of cycles (period is 2, so after 2 seconds this will equal 1); continually grows over time
        float rawSinWave = Mathf.Sin(cycles * tau); // this is going from 1 to -1 and back again (repeatedly)

        movementFactor = (rawSinWave + 1f) / 2f; // converts from (-1 to 1) to (0 to 2), and dividing by 2, so (0 to 1)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
