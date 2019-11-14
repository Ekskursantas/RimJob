using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject magnetWall;
    public GameObject level;

    private void OnCollisionEnter(Collision other)
    {
        //other.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
        other.transform.parent = transform;
        //ObjectSelector.snapped = true;
        // other.transform.position = Vector2.zero;
    }

    private void OnCollisionExit(Collision other)
    {
        //other.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
        other.transform.parent = level.transform;
        // other.transform.position = Vector2.zero;
    }
}