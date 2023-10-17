using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HUBData
{
    public int Enegy;
    public int Star;
    public int Coin;
    public int Level;
}

public class SettingSoundData
{
    public bool isOnMusic;
    public bool isOnSound;
}

public class SaveAndLoadManager : MonoBehaviour
{
    private static SaveAndLoadManager instance;
    public static SaveAndLoadManager getInstance()
    {
        if (instance == null)
        {
            instance = new SaveAndLoadManager();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnApplicationQuit()
    {
        SaveHUBData();
        SaveSoundData();
    }

    // Save HUB Data
    public void SaveHUBData()
    {
        HUBData data = new HUBData();
        data.Enegy = HUBManger.getInstance().enegy;
        data.Star = HUBManger.getInstance().star;
        data.Coin = HUBManger.getInstance().coin;
        data.Level = HUBManger.getInstance().level;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/HUBData.json", json);
    }

    public HUBData LoadHUBData()
    {
        if(File.Exists(Application.persistentDataPath + "/HUBData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/HUBData.json");
            HUBData data = JsonUtility.FromJson<HUBData>(json);
            return data;
        }
        else
        {
            Debug.Log("Not found data: " + Application.persistentDataPath + "/HUBData.json");
            return null;
        }
    }

    // Save Sound Data

    public void SaveSoundData()
    {
        SettingSoundData data = new SettingSoundData();
        data.isOnMusic = SoundManager.getInstance().getOnOffMusic();
        data.isOnSound = SoundManager.getInstance().getOnOffSound();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/SoundData.json", json);
    }

    public SettingSoundData LoadSoundData()
    {
        if (File.Exists(Application.persistentDataPath + "/SoundData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/SoundData.json");
            SettingSoundData data = JsonUtility.FromJson<SettingSoundData>(json);
            return data;
        }
        else
        {
            Debug.Log("Not found data: " + Application.persistentDataPath + "/SoundData.json");
            return null;
        }
    }
}
