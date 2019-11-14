using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RimSelectorHud : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject rimMenu;
    public GameObject primaryCamera;
    public GameObject rimColorSelector;
    public GameObject selector;
    public GameObject garage;
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
    private InfoManager uiControls;

    private void Start()
    {
        uiControls = uiManager.GetComponent<InfoManager>();
        if (Cursor.visible) Cursor.visible = false;
        mainCamera = primaryCamera.GetComponent<CameraLook>();
        spawner = GetComponent<RimSpawner>();
        CP = rimColorSelector.GetComponent<ColorPickerTriangle>();
    }

    void Update()
    {
        if (uiControls.HoldUIIsActive()) return;
        if (!ItemHandler.inRange) return;
        if (!Input.GetKey(KeyCode.R))
        {
            if (disabled) return;
            if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rimMenu.SetActive(false);
            mainCamera.LockCamera(false);
            disabled = true;
            if (!selected) return;

            rimColorSelector.SetActive(false);
            Selector pointer = selector.GetComponent<Selector>();
            colorSelect = false;
            selected = false;
            mouseTravel = new Vector2(0f, 0f);
            uiControls.SetActiveCreationUI(false);
            if(ItemHandler.inRange) uiControls.SetActiveMainUI(true);

            if (pointer.GetSelection() == null) return;
            MenuCollisionHandler handler = pointer.GetSelection().GetComponent<MenuCollisionHandler>();
            if (handler.id < 0)
            {
                handler.Deselect();
                return;
            }

            spawner.Spawn(handler.id, isPainting ? CP.TheColor : Color.clear);
            handler.Deselect();
            isPainting = false;

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
            if (!selected)
            {
                uiControls.SetActiveCreationUI(true);
                uiControls.SetActiveMainUI(false);
                mainCamera.LockCamera(true);
                rimMenu.SetActive(true);
                selected = true;
                disabled = false;
            }
        }

        if (!Input.GetKey(KeyCode.Mouse0)) return;
        if (isPainting) return;
        uiControls.SetActiveCreationUI(false);
        colorSelect = true;
        rimColorSelector.SetActive(true);
        if (!Cursor.visible) Cursor.visible = true;
        if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
        CP.SetNewColor(Color.white);
        isPainting = true;
    }
}