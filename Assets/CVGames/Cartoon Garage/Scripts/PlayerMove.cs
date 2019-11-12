using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f * Mathf.PingPong(Time.time / 2f, 8f) + 4f);
    }
}
