using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "Config/Shop")]
public class ShopConfig : ScriptableObject
{
    private static ShopConfig instance;
    public static ShopConfig getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<ShopConfig>("ScriptableObject/ShopConfig");
        }
        return instance;
    }

    [SerializeField] List<BundleConfig> bundleConfigs = new List<BundleConfig>();
    [SerializeField] List<CoinConfig> coinConfigs = new List<CoinConfig>();

    public BundleConfig getBundleConfig(int ID)
    {
        return bundleConfigs.Find(c => c.ID == ID);
    }

    public List<BundleConfig> getListBundleConfigs()
    {
        return bundleConfigs;
    }

    public CoinConfig getCoinConfig(int ID)
    {
        return coinConfigs.Find(c => c.ID == ID);
    }

    public List<CoinConfig> getListCoinConfigs()
    {
        return coinConfigs;
    }
}

[System.Serializable]
public class BundleConfig
{
    public int ID;
    public string nameTag;
    public float price;
    public List<Reward> rewards = new List<Reward>();
}

[System.Serializable]
public class CoinConfig
{
    public int ID;
    public int coin;
    public float price;
}
