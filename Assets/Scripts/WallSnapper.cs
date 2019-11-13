using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WallSnapper : MonoBehaviour
{
    public Transform snapper;
    public Transform snapper2;
    private float snapDistance = 0.05f;
    public LayerMask magnet;
    public bool snapped = false;
    private bool isSnapped = false;
    private bool isSnapped2 = false;
    // Start is called before the first frame update
    private void Update()
    {
        isSnapped = Physics.CheckSphere(snapper.position, snapDistance, magnet);
        isSnapped2 = Physics.CheckSphere(snapper2.position, snapDistance, magnet);
        if (isSnapped || isSnapped2)
        {
            snapped = true;
        }
    }
}

