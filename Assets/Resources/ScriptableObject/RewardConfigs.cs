using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardConfig", menuName = "Config/Reward")]
public class RewardConfigs : ScriptableObject
{
    private static RewardConfigs instance;
    public static RewardConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<RewardConfigs>("ScriptableObject/RewardConfig");
        }
        return instance;
    }

    [SerializeField] private List<RewardConfig> configs = new List<RewardConfig>();

    public RewardConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<RewardConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class RewardConfig
{
    public int ID;
    public string Name;
    public Sprite img;
}
