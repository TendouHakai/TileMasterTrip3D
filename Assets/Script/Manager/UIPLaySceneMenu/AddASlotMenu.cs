using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddASlotMenu : MonoBehaviour
{
    private void OnEnable()
    {
        GameControler.getInstance().Pause();
    }

    [SerializeField] private FloatToast ToastFrefabs;
    [SerializeField] private GameObject AddICon;
    public void OnBuy500BtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().coin >= 500)
        {
            HUBManger.getInstance().coin -= 500;
            SlotManager.getInstance().addASlot();
            GameControler.getInstance().Resume();
            AddICon.SetActive(false);
            this.gameObject.SetActive(false);
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
        GameControler.getInstance().Resume();
        this.gameObject.SetActive(false);
    }
}
