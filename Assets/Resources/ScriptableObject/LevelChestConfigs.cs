using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelChestConfig", menuName = "Config/LevelChest")]
public class LevelChestConfigs : ScriptableObject
{
    private static LevelChestConfigs instance;
    public static LevelChestConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<LevelChestConfigs>("ScriptableObject/LevelChestConfig");
        }
        return instance;
    }

    [SerializeField] private List<LevelChestConfig> configs = new List<LevelChestConfig>();

    public LevelChestConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<LevelChestConfig> getListConfigs()
    {
        return configs;
    }

    public LevelChestConfig getIsNotClaimConfig()
    {
        return configs.Find(c => c.isClaim == false);
    }

    public LevelChestConfig getPreviousConfigByLevel(int level)
    {
        return configs.FindLast(c => c.level < level);
    }

    public LevelChestConfig getNextConfigByLevel(int level)
    {
        return configs.Find(c => c.level >= level);
    }

    public LevelChestConfig getConfigByLevel(int level)
    {
        return configs.Find(c => c.level == level);
    }
}

[System.Serializable]
public class LevelChestConfig
{
    public int ID;
    public int level;
    public bool isClaim;
    public List<Reward> rewards = new List<Reward>();
}

[System.Serializable]
public class Reward
{
    public int IDRewardConfig;
    public int SL;
}