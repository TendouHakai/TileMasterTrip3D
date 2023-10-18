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

    // Level Chest

    void Start()
    {
        LevelChestMenu.GetComponent<LevelChestMenu>().loadStart();
        StarChestMenu.GetComponent<StarChestMenu>().loadStart();
        LevelChestMenu.SetActive(false);
        StarChestMenu.SetActive(false);
        RewardMenu.SetActive(false);
        ShopMenu.SetActive(false);
    }

    public void OnPlayBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnAddCoinCliCk()
    {
        ShopMenu.SetActive(true);
    }
}
