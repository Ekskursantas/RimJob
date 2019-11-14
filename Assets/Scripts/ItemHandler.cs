using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject menu;
    private Animator anim;
    private bool clicked = false;
    private bool inRange = false;

    private InfoManager uiControls;
    // Start is called before the first frame update
    void Start()
    {
        uiControls = uiManager.GetComponent<InfoManager>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame

    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("return")) && !anim.GetBool("Open") && inRange)
        {
            uiControls.SetActiveCloseGarage(true);
            uiControls.SetActiveOpenGarage(false);
            anim.SetBool("Open", true);
        } else if (Event.current.Equals(Event.KeyboardEvent("return")) && anim.GetBool("Open") && inRange)
        {
            uiControls.SetActiveCloseGarage(false);
            uiControls.SetActiveOpenGarage(true);
            anim.SetBool("Open", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = true;
        uiControls.SetActiveMainUI(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = false;
        uiControls.SetActiveMainUI(false);


    }


    public bool IsInRange()
    {
        return inRange;
    }
}