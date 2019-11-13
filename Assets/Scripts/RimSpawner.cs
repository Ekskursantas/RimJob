using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimSpawner : MonoBehaviour
{
    public GameObject[] rims;
    public GameObject camera;
    private static List<GameObject>  spawnedRims = new List<GameObject>();
    public GameObject gameManager;

    public float distance;

    public void Start()
    {
       if(!MainMenuManager.isNew) gameManager.GetComponent<GameManager>().LoadRims();
    }

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

    public static void DestroyRim(GameObject rim)
    {
        spawnedRims.Remove(rim);
        Destroy(rim);
    }
   
}