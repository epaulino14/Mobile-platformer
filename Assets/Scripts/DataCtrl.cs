using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Singleton class for creating a persistent DataCtrl gameobject.
/// </summary>
public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;
    public GameData data;
    public bool devMode;

    string dataFilePath;
    BinaryFormatter bf;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }
   public void RefreshData()
    {
        if(File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();

            Debug.Log("Data Refreshed");
        }
    }

    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }
    public void SaveData(GameData data)
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }

    public bool isUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].isUnlocked;
    }
    public int getStars(int levelNumber)
    {
        return data.levelData[levelNumber].starsAwarded;
    }
    private void OnEnable()
    {
        CheckDB();
    }
    void CheckDB()
    {
        if(!File.Exists(dataFilePath))
        {
#if UNITY_ANDROID
            CopyDB();
           
#endif

        }
        else
        {

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                string destFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
                File.Delete(destFile);
                File.Copy(dataFilePath, destFile);
            }
            if(devMode)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    File.Delete(dataFilePath);
                    CopyDB();
                }
            }
            
            RefreshData();
        }
    }
    void CopyDB()
    {
        string srcFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
        UnityWebRequest downloader = UnityWebRequest.Get(srcFile);

        while (!downloader.isDone)
        {
            //nothing to be done
        }

        File.WriteAllBytes(dataFilePath, downloader.downloadHandler.data);
        RefreshData();
    }
}
