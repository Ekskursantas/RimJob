using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Rim : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public Vector3 position;
    public Vector3 angle;
    public Color color;

    private void Update()
    {
        position = transform.position;
        angle = transform.rotation.eulerAngles;
        color = GetComponentInChildren<Renderer>().material.color;
    }

    public void UpdateColor()
    {
    }
}
