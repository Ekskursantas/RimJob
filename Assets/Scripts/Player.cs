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

    private void Update()
    {
        savePos = transform.position;
        playerYAngle = transform.rotation.eulerAngles.y;
        cameraXAngle = playerCamera.transform.rotation.eulerAngles.x;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        transform.position = new Vector3(playerData.savePos[0], playerData.savePos[1], playerData.savePos[2]);
        transform.rotation = Quaternion.Euler(0, playerData.playerYangle, 0);
        playerCamera.transform.rotation = Quaternion.Euler(playerData.cameraXangle, 0, 0);
        playerCamera.GetComponent<CameraLook>().resetCamera(); 
    }
}
