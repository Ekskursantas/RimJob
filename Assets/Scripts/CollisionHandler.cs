﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool colliding = false;
    
    
    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Magnet")) return;
        colliding = true;
    }
    
    
    public bool isColliding()
    {
        return colliding;
    }

    public void resetCollision()
    {
        colliding = false;
    }
}
