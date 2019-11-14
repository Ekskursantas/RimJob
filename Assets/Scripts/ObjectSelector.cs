using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public GameObject uiManager;
    public float pickUpDistance;
    public float holdDistance;
    public float smooth;
    public GameObject rimColorSelector;

    private bool carrying = false;
    private bool destroyed = false;
    private RaycastHit carried;
    private Camera cam;
    private CameraLook camLook;
    private bool isPainting = false;
    private ColorPickerTriangle CP;
    private bool release = false;
    private Renderer[] objectRenderer;
    private Vector3 middle;
    private InfoManager uiControls;
    private WallSnapper wheel;

    private void Start()
    {
        cam = GetComponent<Camera>();
        camLook = GetComponent<CameraLook>();
        CP = rimColorSelector.GetComponent<ColorPickerTriangle>();
        middle = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        uiControls = uiManager.GetComponent<InfoManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (uiControls.CreateUIIsActive()) return;
        if (!carrying)
        {
            if (!Input.GetKey(KeyCode.E)) return;
            var ray = cam.ScreenPointToRay(middle);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (hit.collider.CompareTag("Switch"))
            {
                //TO BE DONE
                return;
            }

            if (!(hit.distance < pickUpDistance)) return;
            var selection = hit;
            if (!selection.transform.CompareTag("Selection")) return;
            uiControls.SetActiveHoldUI(true);
            uiControls.SetActiveMainUI(false);
            uiControls.SetActiveChangeColor(true);
            uiControls.SetActiveConfirmColor(false);

            wheel = selection.transform.gameObject.GetComponent<WallSnapper>();
            carried = selection;
            carrying = true;
            release = false;
        }
        else
        {
            if (!destroyed)
            {
                CarryObject(carried);
                //for some reason sometimes this statement does not react and it takes few clicks for it to work....
                if (Input.GetKeyUp(KeyCode.Mouse1) && !isPainting)
                {
                    camLook.LockCamera(true);
                    rimColorSelector.SetActive(true);
                    Cursor.visible = true;
                    if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
                    objectRenderer = carried.transform.gameObject.GetComponentsInChildren<Renderer>();
                    CP.SetNewColor(objectRenderer[0].material.color);
                    isPainting = true;
                    uiControls.SetActiveChangeColor(false);
                    uiControls.SetActiveConfirmColor(true);
                }
                else if (Input.GetKeyUp(KeyCode.Mouse1) && isPainting)
                {
                    camLook.LockCamera(false);
                    rimColorSelector.SetActive(false);
                    Cursor.visible = false;
                    if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
                    isPainting = false;
                }
                else if (Input.GetKey(KeyCode.Delete))
                {
                    uiControls.SetActiveHoldUI(false);
                    if(ItemHandler.inRange) uiControls.SetActiveMainUI(true);
                    RimSpawner.DestroyRim(carried.transform.gameObject);
                    destroyed = true;
                }

                if (isPainting)
                {
                    foreach (Renderer renderer in objectRenderer)
                    {
                        renderer.material.color = CP.TheColor;
                    }
                }
            }


            if (Input.GetKey(KeyCode.E)) return;
            if (release) return;
            camLook.LockCamera(false);
            ;
            rimColorSelector.SetActive(false);
            if (!Cursor.visible) Cursor.visible = false;
            if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
            isPainting = false;
            carrying = false;
            if (destroyed)
            {
                destroyed = false;
                return;
            }

            uiControls.SetActiveHoldUI(false);
            if(ItemHandler.inRange) uiControls.SetActiveMainUI(true);
            Cursor.visible = false;
            carried.rigidbody.useGravity = true;
            release = true;
        }
    }


    void CarryObject(RaycastHit o)
    {
        o.rigidbody.useGravity = false;
        o.rigidbody.velocity = Vector3.zero;
        if (!wheel.snapped)
        {
            o.rigidbody.MovePosition(transform.position + transform.forward * holdDistance);
        }
        else
        {
            o.rigidbody.isKinematic = true;
        }
    }
}