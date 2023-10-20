using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffConfig", menuName = "Config/Buff")]
public class BuffConfigs : ScriptableObject
{
    private static BuffConfigs instance;
    public static BuffConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<BuffConfigs>("ScriptableObject/BuffConfig");
        }
        return instance;
    }

    [SerializeField] List<BuffConfig> configs = new List<BuffConfig>();
    public BuffConfig getConfig(int ID)
    {
        return configs.Find(c => c.ID == ID);
    }

    public List<BuffConfig> getListConfigs()
    {
        return configs;
    }
}

[System.Serializable]
public class BuffConfig
{
    public int ID;
    public string Name;
    public int LevelOpen;
    public Sprite Icon;
    public Sprite IconBuy;
    public string Description;
}
