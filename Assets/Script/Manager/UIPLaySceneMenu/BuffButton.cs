using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuffButton : MonoBehaviour, Observer
{
    [Header("-----------COMPONENT----------------")]
    [SerializeField] Image Icon;
    [SerializeField] Image BackGround;
    [SerializeField] TextMeshProUGUI buffText;

    [Header("-----------BUFF CONFIG--------------")]
    [SerializeField] int buffID;
    BuffConfig config;

    [Header("-----------SPRITE-------------------")]
    [SerializeField] Sprite BackGroundZero;
    [SerializeField] Sprite BackGroundNotZero;
    [SerializeField] Sprite IconLock;
    // Start is called before the first frame update
    void Start()
    {
        HUBManger.getInstance().buffSubject.Attach(this);
        config = BuffConfigs.getInstance().getConfig(buffID);
        Icon.sprite = config.Icon;

        BuffUpdate();
    }

    public virtual void BuffUpdate()
    {
        int SL = HUBManger.getInstance().getSLbuff(buffID);
        if (SL > 0)
        {
            BackGround.sprite = BackGroundNotZero;
            buffText.text = SL.ToString();
        }
        else
        {
            BackGround.sprite = BackGroundZero;
            buffText.text = String.Empty;
        }
    }

    public virtual void OnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().getSLbuff(buffID) > 0)
        {
            HUBManger.getInstance().addBuff(buffID, -1);
            BuffExcute();
        }
        else
        {
            UIPlaySceneManager.getInstance().AddBuffMenu.GetComponent<BuybuffMenu>().buffID = buffID;
            UIPlaySceneManager.getInstance().AddBuffMenu.SetActive(true);
        }
        
    }

    public abstract void BuffExcute();

    public void updateObserver()
    {
        BuffUpdate();
    }
}
