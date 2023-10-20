using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainSceneManager : MonoBehaviour
{
    private static UIMainSceneManager instance;
    public static UIMainSceneManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<UIMainSceneManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public GameObject LevelChestMenu;
    [SerializeField] public GameObject StarChestMenu;
    [SerializeField] public GameObject ShopMenu;
    [SerializeField] public GameObject RewardMenu;
    [SerializeField] public GameObject AdsMenu;
    [SerializeField] public GameObject SettingMenu;

    // Level Chest

    void Start()
    {
        LevelChestMenu.GetComponent<LevelChestMenu>().loadStart();
        StarChestMenu.GetComponent<StarChestMenu>().loadStart();
        LevelChestMenu.SetActive(false);
        StarChestMenu.SetActive(false);
        RewardMenu.SetActive(false);
        ShopMenu.SetActive(false);
        AdsMenu.SetActive(false);
        SettingMenu.SetActive(false);
    }

    public void OnPlayBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        SaveAndLoadManager.getInstance().SaveHUBData();
        SaveAndLoadManager.getInstance().SaveSoundData();
        SceneManager.LoadScene(1);
    }

    public void OnAddCoinCliCk()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        ShopMenu.SetActive(true);
    }

    // Ads
    public void OpenAdsMenu()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        AdsMenu.SetActive(true);
    }

    public void CloseAdsMenu()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        AdsMenu.SetActive(false);
    }

    // Setting menu
    public void OpenSettingMenu()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        SettingMenu.SetActive(true);
    }
}
