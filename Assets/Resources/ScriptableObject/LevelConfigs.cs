using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/Level")]
public class LevelConfigs : ScriptableObject
{
    private static LevelConfigs instance;
    public static LevelConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<LevelConfigs>("ScriptableObject/LevelConfig");
        }
        return instance;
    }

    [SerializeField] private List<LevelConfig> configs = new List<LevelConfig>();

    public LevelConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<LevelConfig> getListConfigs()
    {
        return configs;
    }

    public bool isHaveLevel(int ID)
    {
        return configs.Find(c => c.ID == ID) != null;
    }
}

[System.Serializable]
public class LevelConfig
{
    public int ID;
    public string Name;
    public int Time;
    public List<TileInLevel> tileInLevels = new List<TileInLevel>();

    public LevelConfig()
    {
        ID = 0;
        Name = "New level";
        Time = 100;
    }

    public LevelConfig(int ID)
    {
        this.ID = ID;
        Name = "New level";
        Time = 100;
    }
    
}

[System.Serializable]
public class TileInLevel
{
    public string IDTile;
    public int chain;

    public TileInLevel()
    {
        IDTile = "";
        chain = 0;
    }

    public TileInLevel(string iDTile, int count = 0)
    {
        IDTile = iDTile;
        this.chain = count;
    }
}
