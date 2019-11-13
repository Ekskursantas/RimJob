using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool colliding = false;
    private Rigidbody body;

    private void Start()
    {
        
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        colliding = true;
    }

    private void OnCollisionExit(Collision other)
    {
        
    }


    public bool IsColliding()
    {
        return colliding;
    }

    public void ResetCollision()
    {
        colliding = false;
    }
}
