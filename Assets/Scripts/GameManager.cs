using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject rimHandler;
    public GameObject playerCamera;
    private Movement move;
    private List<GameObject> spawnedRims;
    private Player _player;
    private RimSpawner _rimSpawner;
    private GameObject[] rims;

    public void Awake()
    {
        move = player.GetComponent<Movement>();
        _player = player.GetComponent<Player>();
        _rimSpawner = rimHandler.GetComponent<RimSpawner>();
        spawnedRims = _rimSpawner.ExistingRims();
        rims = _rimSpawner.rims;

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(_player);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        move.LoadPosition(new Vector3(playerData.savePos[0], playerData.savePos[1], playerData.savePos[2]));
        Debug.Log(playerData.playerYangle);
        player.transform.localRotation = Quaternion.Euler(0, playerData.playerYangle, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(playerData.cameraXangle, 0, 0);
        playerCamera.GetComponent<CameraLook>().resetCamera();
    }

    public void SaveRims()
    {
        SaveSystem.SaveRims(spawnedRims);
    }

    public void LoadRims()
    {
        if (spawnedRims.Count > 0)
        {
            foreach (GameObject rim in spawnedRims)
            {
                Destroy(rim);
            }

            spawnedRims.Clear();
        }

        RimData loadedRims = SaveSystem.LoadData();
        for (int i = 0; i < loadedRims.savePos.GetLength(0); i++)
        {
            GameObject rim = Instantiate(rims[loadedRims.id[i]],
                new Vector3(loadedRims.savePos[i, 0], loadedRims.savePos[i, 1], loadedRims.savePos[i, 2]),
                Quaternion.Euler(loadedRims.saveAngle[i, 0], loadedRims.saveAngle[i, 1], loadedRims.saveAngle[i, 2]));
            rim.GetComponent<Rigidbody>().isKinematic = loadedRims.isKinematic[i];
            spawnedRims.Add(rim);

            Renderer[] renderers = rim.GetComponentsInChildren<Renderer>();
            for (int j = 0; j < loadedRims.saveColor[i].Length; j++)
            {
                if (ColorUtility.TryParseHtmlString("#" + loadedRims.saveColor[i][j], out Color tint))
                {
                    Debug.Log(tint);
                    if (tint.Equals(Color.clear)) return;
                    renderers[j].material.color = tint;
                }
            }

        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
    

