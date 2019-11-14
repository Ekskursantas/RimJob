using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 savePos;
    public float playerYAngle;
    public float cameraXAngle;
    public GameObject playerCamera;
    public GameObject gameManager;
    private Movement move;

    private void Start()
    {
        if (!MainMenuManager.isNew) gameManager.GetComponent<GameManager>().LoadPlayer();
    }

    private void Update()
    {
        savePos = transform.localPosition;
        playerYAngle = transform.localEulerAngles.y;
        cameraXAngle = playerCamera.transform.localEulerAngles.x;
    }
}