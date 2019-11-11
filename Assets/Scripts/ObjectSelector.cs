using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public float distance;
    private bool carrying = false;
    private bool destroyed = false;
    private CollisionHandler col;
    private RaycastHit carried;
    public float smooth;

    // Update is called once per frame
    void Update()
    {
        if (!carrying)
        {
            if (!Input.GetKey(KeyCode.E)) return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!(hit.distance < distance)) return;
            var selection = hit;
            if (!selection.transform.CompareTag("Selection")) return;
            col = selection.transform.gameObject.GetComponent<CollisionHandler>();
            if (col.isColliding()) col.resetCollision();
            carried = selection;
            carrying = true;
        }
        else
        {
            if (!destroyed)
            {
                if (!col.isColliding())
                {
                    carryObject(carried);
                    if (Input.GetKey(KeyCode.Mouse1))
                    {
                        Destroy(carried.transform.gameObject);
                        destroyed = true;
                    }
                }
                else
                {
                    carried.rigidbody.useGravity = true;
                }
            }

            if (Input.GetKey(KeyCode.E)) return;
            carrying = false;
            if (destroyed)
            {
                destroyed = false;
                return;
            }
            carried.rigidbody.useGravity = true;
        }
    }

    void carryObject(RaycastHit o)
    {
        o.rigidbody.useGravity = false;
        o.transform.position = transform.position + transform.forward * distance;
    }
}