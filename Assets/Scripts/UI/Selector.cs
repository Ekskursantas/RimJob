using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Selector : MonoBehaviour
{    
    private Vector2 mouseTravel;
    private Vector2 smooth;
    private Vector2 lastValidMouseMove;
    private Vector2 mouse;    
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;
    private GameObject sellection;

    public void setSelection(GameObject obj)
    {
        sellection = obj;
    }

    public GameObject getSelection()
    {
        return sellection;
    }
}
