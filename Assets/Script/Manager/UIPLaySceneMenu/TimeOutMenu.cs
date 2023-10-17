using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOutMenu : MonoBehaviour
{
    [SerializeField] private FloatToast ToastFrefabs;
    public void OnBuy500BtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().coin >= 500)
        {
            HUBManger.getInstance().coin -=500;
            HUBManger.getInstance().addTime();
            this.gameObject.SetActive(false);
            GameControler.getInstance().Resume();
        }
        else
        {
            FloatToast toast = Instantiate(ToastFrefabs);
            toast.transform.SetParent(gameObject.transform);
        }
    }

    public void OnCloseBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        UIPlaySceneManager.getInstance().playLevelMenu.SetActive(true);
        UIPlaySceneManager.getInstance().playLevelMenu.GetComponent<PLayMenu>().BtnText.text = "Try Again";
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameControler.getInstance().Pause();
    }
}
