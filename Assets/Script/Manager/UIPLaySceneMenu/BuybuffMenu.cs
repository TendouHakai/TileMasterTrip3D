using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuybuffMenu : MonoBehaviour
{
    [SerializeField] public int buffID;
    BuffConfig config;

    [Header("---------------COMPONENT----------------")]
    [SerializeField] TextMeshProUGUI buffNameText;
    [SerializeField] Image buffIcon;

    [Header("---------------FREFABS------------------")]
    [SerializeField] FloatToast ToastFrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCloseBtn()
    {
        this.gameObject.SetActive(false);
    }

    public void OnBuy500BtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().coin >= 500)
        {
            HUBManger.getInstance().coin -= 500;
            HUBManger.getInstance().addBuff(buffID, 1);
            GameControler.getInstance().Resume();
            this.gameObject.SetActive(false);
        }
        else
        {
            FloatToast toast = Instantiate(ToastFrefabs);
            toast.transform.SetParent(gameObject.transform);
        }
    }

    private void OnEnable()
    {
        config = BuffConfigs.getInstance().getConfig(buffID);
        buffNameText.text = "Add a" + config.Name;
        buffIcon.sprite = config.IconBuy;
    }
}
