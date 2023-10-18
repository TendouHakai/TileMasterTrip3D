using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    //Scroll View
    [SerializeField] RectTransform ViewPortContainer;
    [SerializeField] RectTransform BundleContainer;
    [SerializeField] RectTransform CoinBuyContainer;

    [Header("--------------FREFABS------------")]
    [SerializeField] Bundle BundleFrefab;
    [SerializeField] CoinBuy CoinBuyFrefab;

    private void Start()
    {
        coinText.text = HUBManger.getInstance().coin.ToString();

        List<BundleConfig> bundleConfigs = ShopConfig.getInstance().getListBundleConfigs();
        List<CoinConfig> coinConfigs = ShopConfig.getInstance().getListCoinConfigs();

        Debug.Log(bundleConfigs);

        foreach (BundleConfig i in bundleConfigs)
        {
            Bundle bundle = Instantiate(BundleFrefab);
            bundle.IDBundle = i.ID;
            bundle.transform.SetParent(BundleContainer.transform);
        }

        foreach (CoinConfig i in coinConfigs)
        {
            CoinBuy coinBuy = Instantiate(CoinBuyFrefab);
            coinBuy.IDCoinConfig = i.ID;
            coinBuy.transform.SetParent(CoinBuyContainer.transform);
        }

        // update size container
        Vector2 size1= BundleContainer.sizeDelta;
        size1.y = Mathf.CeilToInt(bundleConfigs.Count / 2)* BundleContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        BundleContainer.sizeDelta = size1;

        Vector2 size2 = CoinBuyContainer.sizeDelta;
        size2.y = Mathf.CeilToInt(bundleConfigs.Count / 3) * CoinBuyContainer.GetComponent<GridLayoutGroup>().cellSize.y;
        CoinBuyContainer.sizeDelta = size2;

        Vector2 size3 = ViewPortContainer.sizeDelta;
        size3.y  = size1.y + size2.y;
        ViewPortContainer.sizeDelta = size3;  
    }

    public void OnCloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }
}
