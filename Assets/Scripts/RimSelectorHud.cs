using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RimSelectorHud : MonoBehaviour
{
    public GameObject rimMenu;
    public GameObject primaryCamera;
    public GameObject rimColorSelector;
    public GameObject selector;
    public ButtonHandler buttonHandler;
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;

    private CameraLook mainCamera;
    private Vector2 mouseTravel;
    private Vector2 smooth;
    private Vector2 lastValidMouseMove;
    private Vector2 mouse;
    private bool selected = false;
    private RimSpawner spawner;
    private bool colorSelect = false;
    private ColorPickerTriangle CP;
    private bool isPainting = false;
    private bool disabled = true;


    private void Start()
    {
        if (Cursor.visible) Cursor.visible = false;
        mainCamera = primaryCamera.GetComponent<CameraLook>();
        spawner = GetComponent<RimSpawner>();
        CP = rimColorSelector.GetComponent<ColorPickerTriangle>();
        GameObject[] buttons = buttonHandler.buttons;
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            if (disabled) return;
            if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
            rimMenu.SetActive(false);
            mainCamera.LockCamera(false);
            disabled = true;
            if (!selected) return;
            Selector pointer = selector.GetComponent<Selector>();
            MenuCollisionHandler handler = pointer.GetSelection().GetComponent<MenuCollisionHandler>();
            if (handler.id < 0)
            {
                handler.Deselect();
                return;
            }

            spawner.Spawn(handler.id, isPainting ? CP.TheColor : Color.clear);
            rimColorSelector.SetActive(false);
            colorSelect = false;
            selected = false;
            handler.Deselect();
            Cursor.visible = false;
            isPainting = false;
            mouseTravel = new Vector2(0f, 0f);
            return;
        }

        if (!colorSelect)
        {
            if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
            mouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            mouse = Vector2.Scale(mouse, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
            smooth.x = Mathf.Lerp(smooth.x, mouse.x, 1f / smoothness);
            smooth.y = Mathf.Lerp(smooth.y, mouse.y, 1f / smoothness);
            mouseTravel += smooth;
            selector.transform.localRotation = Quaternion.AngleAxis(-mouseTravel.x, Vector3.forward);
            mainCamera.LockCamera(true);
            rimMenu.SetActive(true);
            selected = true;
            disabled = false;
        }

        if (!Input.GetKey(KeyCode.Mouse0)) return;
        if (isPainting) return;
        colorSelect = true;
        rimColorSelector.SetActive(true);
        if (!Cursor.visible) Cursor.visible = true;
        if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
        CP.SetNewColor(Color.white);
        isPainting = true;
    }
}