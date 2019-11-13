using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    // Start is called before the first frame update
    public static void SaveProgress(List<GameObject> rimData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.data";
        FileStream stream = new FileStream(path, FileMode.Create);
       
        
        RimData data = new RimData(rimData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static RimData LoadData()
    {
        string path = Application.persistentDataPath + "/game.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RimData rimsLoaded = formatter.Deserialize(stream) as RimData;
            stream.Close();
            Debug.Log(rimsLoaded);
            return rimsLoaded;
        }
        else
        {
            return null;
        }
    }
}
