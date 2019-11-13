using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public float distance;
    public float smooth;
    public GameObject rimColorSelector;
    private bool carrying = false;
    private bool destroyed = false;
    private CollisionHandler col;
    private RaycastHit carried;
    private Camera cam;
    private CameraLook camLook;
    private bool isPainting = false;
    private ColorPickerTriangle CP;
    private bool release = false;
    private Renderer objectRenderer;
    private void Start()
    {
        cam = GetComponent<Camera>();
        camLook = GetComponent<CameraLook>();
        CP = rimColorSelector.GetComponent<ColorPickerTriangle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!carrying)
        {
            if (!Input.GetKey(KeyCode.E)) return;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (hit.collider.CompareTag("Switch"))
            {
                
                return;
            }
            if (!(hit.distance < distance)) return;
            var selection = hit;
            if (!selection.transform.CompareTag("Selection")) return;
            col = selection.transform.gameObject.GetComponent<CollisionHandler>();
            if (col.IsColliding()) col.ResetCollision();
            carried = selection;
            carrying = true;
            release = false;
        }
        else
        {
            if (!destroyed)
            {
                if (!col.IsColliding())
                {

                    CarryObject(carried);

                    if (Input.GetKeyUp(KeyCode.Mouse2) && !isPainting)
                    {
                        camLook.LockCamera(true);
                        rimColorSelector.SetActive(true);
                        if (!Cursor.visible) Cursor.visible = true;
                        if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
                        objectRenderer = carried.transform.gameObject.GetComponentInChildren<Renderer>();
                        CP.SetNewColor(objectRenderer.material.color);
                        isPainting = true;
                    } else if (Input.GetKeyUp(KeyCode.Mouse2) && isPainting)
                    {
                        camLook.LockCamera(false);;
                        rimColorSelector.SetActive(false);
                        if (!Cursor.visible) Cursor.visible = false;
                        if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
                        isPainting = false;
                    }
                    else if (Input.GetKey(KeyCode.Mouse1))
                    {
                        RimSpawner.DestroyRim(carried.transform.gameObject);
                        destroyed = true;
                    }
                    if (isPainting) objectRenderer.material.color = CP.TheColor;
                }
                else
                {
                    carried.rigidbody.useGravity = true;
                }
            }


            if (Input.GetKey(KeyCode.E)) return;
            if (release) return;
            camLook.LockCamera(false);;
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

            carried.rigidbody.useGravity = true;
            release = true;
        }
    }


    void CarryObject(RaycastHit o)
    {
        o.rigidbody.useGravity = false;
        o.transform.position = transform.position + transform.forward * distance;
    }
}