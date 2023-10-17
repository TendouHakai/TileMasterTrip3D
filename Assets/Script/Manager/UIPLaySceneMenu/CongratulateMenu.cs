using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CongratulateMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI StarText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCloseClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        UIPlaySceneManager.getInstance().playLevelMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnContinueBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        UIPlaySceneManager.getInstance().playLevelMenu.SetActive(true); 
        UIPlaySceneManager.getInstance().playLevelMenu.GetComponent<PLayMenu>().BtnText.text = "PLay";
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StarText.text = HUBManger.getInstance().starInLevel.ToString();
    }
}
