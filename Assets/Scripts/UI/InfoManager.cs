using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public GameObject creationUI;
    public GameObject holdUI;
    public GameObject mainUI;
    public GameObject openGarage;
    public GameObject closeGarage;
    public GameObject changeColor;
    public GameObject confirmColor;

    public void SetActiveCreationUI(bool value)
    {
        creationUI.SetActive(value);
    }

    public void SetActiveHoldUI(bool value)
    {
        holdUI.SetActive(value);
    }

    public void SetActiveMainUI(bool value)
    {
        mainUI.SetActive(value);
    }

    public void SetActiveOpenGarage(bool value)
    {
        openGarage.SetActive(value);
    }

    public void SetActiveCloseGarage(bool value)
    {
        closeGarage.SetActive(value);
    }

    public void SetActiveChangeColor(bool value)
    {
        changeColor.SetActive(value);
    }

    public void SetActiveConfirmColor(bool value)
    {
        confirmColor.SetActive(value);
    }

    public bool CreateUIIsActive()
    {
        return creationUI.activeSelf;
    }

    public bool HoldUIIsActive()
    {
        return holdUI.activeSelf;
    }
}