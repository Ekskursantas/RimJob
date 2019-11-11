using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimSpawner : MonoBehaviour
{
    public GameObject[] rims;
    public GameObject camera;

    public float distance;
    
    // Update is called once per frame

    public void spawn(int id)
    {
        Instantiate(rims[id], camera.transform.position + camera.transform.forward*distance, camera.transform.rotation);
    }
}
