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
            Cursor.visible = menu.activeSelf;
            if (Cursor.lockState != CursorLockMode.None && menu.activeSelf) Cursor.lockState = CursorLockMode.None;
            else if (Cursor.lockState != CursorLockMode.Locked && !menu.activeSelf)
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void CloseUI()
    {
        menu.SetActive(false);
        Cursor.visible = menu.activeSelf;
        cam.LockCamera(false);
        if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
    }
}