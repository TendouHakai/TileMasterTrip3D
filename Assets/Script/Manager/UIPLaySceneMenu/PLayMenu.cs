using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] public TextMeshProUGUI BtnText;

    public void OnCloseBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        UIPlaySceneManager.getInstance().BackToMainScene();
        //SpawnManager.getInstance().loadLevel(HUBManger.getInstance().level);
        //GameControler.getInstance().Resume();
        //this.gameObject.SetActive(false);
    }

    public void OnPlayBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        SpawnManager.getInstance().loadLevel(HUBManger.getInstance().level);
        GameControler.getInstance().Resume();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        LevelText.text = "LEVEL " + HUBManger.getInstance().level.ToString();
    }
}
