using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public float pickUpDistance;
    public float holdDistance;
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
    private Renderer[] objectRenderer;
    public WallSnapper wheel;
    private Vector3 middle;
    private void Start()
    {
        cam = GetComponent<Camera>();
        camLook = GetComponent<CameraLook>();
        CP = rimColorSelector.GetComponent<ColorPickerTriangle>();
        middle = new Vector3(Screen.width/2, Screen.height/2, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                    if (Input.GetKeyUp(KeyCode.Mouse1) && !isPainting)
                    {
                        camLook.LockCamera(true);
                        rimColorSelector.SetActive(true);
                        if (!Cursor.visible) Cursor.visible = true;
                        if (Cursor.lockState != CursorLockMode.None) Cursor.lockState = CursorLockMode.None;
                        objectRenderer = carried.transform.gameObject.GetComponentsInChildren<Renderer>();
                        CP.SetNewColor(objectRenderer[0].material.color);
                        isPainting = true;
                    } else if (Input.GetKeyUp(KeyCode.Mouse1) && isPainting)
                    {
                        camLook.LockCamera(false);;
                        rimColorSelector.SetActive(false);
                        if (!Cursor.visible) Cursor.visible = false;
                        if (Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
                        isPainting = false;
                    }
                    else if (Input.GetKey(KeyCode.Delete))
                    {
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