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
}

[System.Serializable]
public class LevelConfig
{
    public int ID;
    public string Name;
    public int Time;

    public LevelConfig()
    {
        ID = 0;
        Name = "New level";
        Time = 0;
    }
    public List<TileInLevel> tileInLevels = new List<TileInLevel>();
}

[System.Serializable]
public class TileInLevel
{
    public int IDTile;
    public int count;

    public TileInLevel()
    {
        IDTile = 0;
        count = 0;
    }

    public TileInLevel(int iDTile, int count = 0)
    {
        IDTile = iDTile;
        this.count = count;
    }
}
