using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour
{
    [Header("------------COMPONENT----------")]
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI textSL;

    [Header("------------INFO----------")]
    public int ID;
    public int SL;
    // Start is called before the first frame update
    void Start()
    {
        RewardConfig config = RewardConfigs.getInstance().getConfig(ID);
        img.sprite = config.img;
        textSL.text = "X"+SL.ToString();
    }

}
