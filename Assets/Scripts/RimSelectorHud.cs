using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RimSelectorHud : MonoBehaviour
{
    public GameObject rimMenu;
    public GameObject camera;
    private CameraLook cam;
    private Vector2 mouseTravel;
    private Vector2 smooth;
    private Vector2 lastValidMouseMove;
    private Vector2 mouse;
    private bool selected = false;
    private Rect[] buttonRect;
    private RimSpawner spawner;
    public GameObject selector;
    public ButtonHandler buttonHandler;
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;

    private void Start()
    {
        if (Cursor.visible) Cursor.visible = false;
        cam = camera.GetComponent<CameraLook>();
        spawner = GetComponent<RimSpawner>();
        GameObject[] buttons = buttonHandler.buttons;
        buttonRect = new Rect[buttons.Length];
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
            rimMenu.SetActive(false);
            cam.unlockCamera();
            if (!selected) return;
            Selector pointer = selector.GetComponent<Selector>();
            MenuCollisionHandler handler = pointer.getSelection().GetComponent<MenuCollisionHandler>();
            if (handler.id < 0)
            {
                handler.deselect();
                return;
            }
            spawner.spawn(handler.id);
            selected = false;
            handler.deselect();
            mouseTravel = new Vector2(0f, 0f);
            return;
        }

        if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
        mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouse = Vector2.Scale(mouse, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
        smooth.x = Mathf.Lerp(smooth.x, mouse.x, 1f / smoothness);
        smooth.y = Mathf.Lerp(smooth.y, mouse.y, 1f / smoothness);
        mouseTravel += smooth;
        selector.transform.localRotation = Quaternion.AngleAxis(-mouseTravel.x, Vector3.forward);
        cam.lockCamera();
        rimMenu.SetActive(true);
        selected = true;
    }
}