using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartChestConfig", menuName = "Config/StarChest")]
public class StartChestConfigs : ScriptableObject
{
    private static StartChestConfigs instance;
    public static StartChestConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<StartChestConfigs>("ScriptableObject/StartChestConfig");
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
}

[System.Serializable]
public class StarChestConfig
{
    public int ID;
    public int Star;
    public bool isClaim;
    public List<Reward> rewards = new List<Reward>();
}
