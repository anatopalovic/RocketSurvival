using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 0.4f)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (period != Mathf.Epsilon)
        {
            float cycles = Time.time / period;

            const float tau = Mathf.PI * 2;
            float rawSineWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSineWave + 1f) / 2f;

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }

    }
}
