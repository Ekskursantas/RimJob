using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
        other.transform.parent = transform;
    }
}
