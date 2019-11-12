using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform;
    }
}
