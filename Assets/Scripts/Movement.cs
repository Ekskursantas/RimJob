using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float strafe = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Translate(strafe, 0f, movement);
    }
}
