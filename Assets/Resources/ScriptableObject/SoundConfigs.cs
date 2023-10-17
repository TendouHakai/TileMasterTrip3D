using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Config/Sound")]
public class SoundConfigs : ScriptableObject
{
    private static SoundConfigs instance;
    public static SoundConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<SoundConfigs>("ScriptableObject/SoundConfig");
        }
        return instance;
    }

    [SerializeField] private List<SoundConfig> configs = new List<SoundConfig>();

    public SoundConfig getConfig(string ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<SoundConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class SoundConfig
{
    public string ID;
    public AudioClip clip;
}
