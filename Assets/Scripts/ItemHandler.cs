using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public GameObject garage;

    private Animator anim;

    private bool clicked = false;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = garage.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Open"))
        {
            anim.SetBool("Open", true);
        } else if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Open"))
        {
            anim.SetBool("Open", false);
        }
        
    }
}