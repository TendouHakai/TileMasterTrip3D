using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileConfig", menuName = "Config/Tile")]
public class TileConfigs : ScriptableObject
{
    private static TileConfigs instance;
    public static TileConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<TileConfigs>("ScriptableObject/TileConfig");
        }
        return instance;
    }

    [SerializeField] private List<TileConfig> configs = new List<TileConfig>();

    public TileConfig getFirstConfig()
    {
        if(configs.Count != 0) return configs[0];
        else return null;
    }

    public TileConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<TileConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class TileConfig
{
    public int ID;
    public Texture2D img;

    public TileConfig()
    {
        ID = 0;
    }
}
