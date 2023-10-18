using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarChestConfig", menuName = "Config/StarChest")]
public class StarChestConfigs : ScriptableObject
{
    private static StarChestConfigs instance;
    public static StarChestConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<StarChestConfigs>("ScriptableObject/StarChestConfig");
        }
        return instance;
    }

    [SerializeField] private List<StarChestConfig> configs = new List<StarChestConfig>();

    public StarChestConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<StarChestConfig> getListConfigs()
    {
        return configs;
    }

    public StarChestConfig getIsNotClaimConfig()
    {
        return configs.Find(c => c.isClaim == false);
    }

    public StarChestConfig getPreviousConfigByStar(int Star)
    {
        return configs.FindLast(c => c.Star < Star);
    }
}

[System.Serializable]
public class StarChestConfig
{
    public int ID;
    public int Star;
    public bool isClaim;
    public List<Reward> rewards = new List<Reward>();
}
