using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{

    private Animator anim;

    private bool clicked = false;
    private bool opened = false;

    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Open") && inRange)
        {
            anim.SetBool("Open", true);
        } else if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Open") && inRange)
        {
            anim.SetBool("Open", false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = false;
    }


    public bool IsInRange()
    {
        return inRange;
    }
}