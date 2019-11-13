using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] savePos;
    public float playerYangle;
    public float cameraXangle;

    public PlayerData(Player player)
    {
        savePos = new float[3];
        savePos[0] = player.savePos.x;
        savePos[1] = player.savePos.y;
        savePos[2] = player.savePos.z;
        playerYangle = player.playerYAngle;
        cameraXangle = player.cameraXAngle;
    }
}
