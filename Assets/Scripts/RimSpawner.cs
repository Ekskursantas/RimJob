using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimSpawner : MonoBehaviour
{
    public GameObject[] rims;
    public GameObject camera;
    private List<GameObject> spawnedRims = new List<GameObject>();


    public float distance;

    // Update is called once per frame

    public void Spawn(int id, Color tint)
    {
        if (spawnedRims.Count > 35)
        {
            GameObject firstRim = spawnedRims[0];
            Destroy(firstRim);
            spawnedRims.RemoveAt(0);
            Debug.Log("Removed");
        }

        GameObject rim = Instantiate(rims[id], camera.transform.position + camera.transform.forward * distance,
            camera.transform.rotation);
        spawnedRims.Add(rim);
        if (tint.Equals(Color.clear)) return;
        Renderer[] renderers = rim.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = tint;
        }
    }

    public List<GameObject> ExistingRims()
    {
        return spawnedRims;
    }

    public void SaveRims()
    {
        SaveSystem.SaveProgress(spawnedRims);
    }

    public void LoadRims()
    {
        if (spawnedRims.Count > 0)
        {
            foreach (GameObject rim in spawnedRims)
            {
                Destroy(rim);
            }
        }
        RimData loadedRims = SaveSystem.LoadData();
        for (int i = 0; i < loadedRims.savePos.GetLength(0); i++)
        {
            Debug.Log(loadedRims.savePos.Length);
            GameObject rim = Instantiate(rims[loadedRims.id[i]], new Vector3(loadedRims.savePos[i,0],loadedRims.savePos[i,1],loadedRims.savePos[i,2]),
                Quaternion.Euler(loadedRims.saveAngle[i,0],loadedRims.saveAngle[i,1],loadedRims.saveAngle[i,2]));
            spawnedRims.Add(rim);
            ColorUtility.TryParseHtmlString(loadedRims.saveColor[i], out Color tint);
            if (tint.Equals(Color.clear)) return;
            Renderer[] renderers = rim.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = tint;
            }
        }
    }
}