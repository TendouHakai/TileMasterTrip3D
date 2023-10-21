using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBuy : MonoBehaviour
{
    [SerializeField] public int IDCoinConfig;
    CoinConfig config;

    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        config = ShopConfig.getInstance().getCoinConfig(IDCoinConfig);
        if(config !=  null)
        {
            coinText.text = config.coin.ToString();
            priceText.text = config.price.ToString();
        }
        transform.localScale = new Vector3(1, 1, 1);
    }

}
