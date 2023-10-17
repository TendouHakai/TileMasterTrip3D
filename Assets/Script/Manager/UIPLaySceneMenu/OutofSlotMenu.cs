using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofSlotMenu : MonoBehaviour
{
    [SerializeField] private FloatToast ToastFrefabs;
    public void OncloseBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        UIPlaySceneManager.getInstance().playLevelMenu.SetActive(true);
        UIPlaySceneManager.getInstance().playLevelMenu.GetComponent<PLayMenu>().BtnText.text = "Try Again";
        this.gameObject.SetActive(false);
    }

    public void OnBuy500BtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().coin >= 500)
        {
            SlotManager.getInstance().Remove(3);
            this.gameObject.SetActive(false);
            GameControler.getInstance().Resume();
        }
        else
        {
            FloatToast toast = Instantiate(ToastFrefabs);
            toast.transform.SetParent(gameObject.transform);
        }
    }

    private void OnEnable()
    {
        GameControler.getInstance().Pause();
    }
}
