using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RimData
{
    public int[] id;
    public float[,] savePos;
    public string[] saveColor;
    public float[,] saveAngle;

    // Start is called before the first frame update
    public RimData(List<GameObject> rimData)
    {
        id = new int[rimData.Count];
        savePos = new float[rimData.Count,3];
        saveAngle = new float[rimData.Count,3];
        saveColor = new string[rimData.Count];
        for (int i = 0; i < rimData.Count; i++)
        {
            Rim rim = rimData[i].GetComponent<Rim>();
            id[i] = rim.id;
            savePos[i, 0] = rim.position.x;
            savePos[i, 1] = rim.position.y;
            savePos[i, 2] = rim.position.z;
            saveColor[i] = ColorUtility.ToHtmlStringRGBA(rim.color);
            saveAngle[i, 0] = rim.angle.x;
            saveAngle[i, 1] = rim.angle.y;
            saveAngle[i, 2] = rim.angle.z;
        }

    }
}
