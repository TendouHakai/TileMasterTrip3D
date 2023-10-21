using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bundle : MonoBehaviour
{
    [SerializeField] public int IDBundle;
    BundleConfig config;

    [SerializeField] TextMeshProUGUI TagText;
    [SerializeField] TextMeshProUGUI cointText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] GameObject grid;

    [SerializeField] RewardItem RewardsFrefab;

    // Start is called before the first frame update
    void Start()
    {
        config = ShopConfig.getInstance().getBundleConfig(IDBundle);
        if(config != null)
        {
            TagText.text = config.nameTag;
            PriceText.text = "$"+config.price.ToString();
            Debug.Log(config.rewards.Count);
            for(int i =0; i<config.rewards.Count; i++)
            {
                if (config.rewards[i].IDRewardConfig == 0)
                {
                    cointText.text = config.rewards[i].SL.ToString();
                }
                else
                {
                    RewardItem item = Instantiate(RewardsFrefab);
                    item.ID = config.rewards[i].IDRewardConfig;
                    item.SL = config.rewards[i].SL;

                    item.transform.SetParent(grid.transform);
                }
            }
        }
        transform.localScale = new Vector3(1, 1, 1);
    }
}
