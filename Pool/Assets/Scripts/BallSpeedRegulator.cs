using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedRegulator : MonoBehaviour
{
    public bool isScored = false;

    private Rigidbody rb;
    public float currentSpeed;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        if (currentSpeed < 0.07f)
        {
            rb.velocity = new Vector3(0, 0, 0);
            currentSpeed = rb.velocity.magnitude;
        }
    }

    public bool IsStationary()
    {
        if (currentSpeed < 0.01f || isScored)
            return true;
        else
            return false;
    }
}
