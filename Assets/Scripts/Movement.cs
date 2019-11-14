using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController charCon;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        charCon.Move(move * Time.deltaTime * speed);
        velocity.y += gravity * Time.deltaTime;
        charCon.Move(velocity * Time.deltaTime);
    }

    public void LoadPosition(Vector3 pos)
    {
        charCon.enabled = false;
        transform.localPosition = pos;
        charCon.enabled = true;
    }
}