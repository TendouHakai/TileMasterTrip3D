using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackConfig", menuName = "Config/Pack")]
public class PackConfigs : ScriptableObject
{
    private static PackConfigs instance;
    public static PackConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<PackConfigs>("ScriptableObject/PackConfig");
        }
        return instance;
    }

    [SerializeField] private List<PackConfig> configs = new List<PackConfig>();

    public PackConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public PackConfig getConfigByLevel(int level)
    {
        return configs.Find(c => c.Level == level);
    }

    public PackConfig getPreviousConfigByLevel(int level)
    {
        return configs.FindLast(c => c.Level <= level);
    }

    public PackConfig getNextConfigByLevel(int level)
    {
        return configs.Find(c => c.Level > level);
    }

    public List<PackConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class PackConfig
{
    public int ID;
    public int Level;
    public List<string> TileOpens = new List<string>();
}
