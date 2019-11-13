using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject menu;
    public GameObject camera;

    private CameraLook cam;

    private void Start()
    {
        cam = camera.GetComponent<CameraLook>();
    }

    private void OnGUI()
    {

        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            menu.SetActive(!menu.activeSelf);
            cam.LockCamera(menu.activeSelf);
            if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CloseUI()
    {
        menu.SetActive(false);
        cam.LockCamera(false);
        if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
    }
}
