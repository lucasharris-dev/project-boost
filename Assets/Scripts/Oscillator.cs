using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector = new Vector3(0f, -15f, 0f);
    [SerializeField] [Range(0f,1f)] float movementFactor;

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
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
