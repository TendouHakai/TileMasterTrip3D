using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlaySceneManager : MonoBehaviour
{
    private static UIPlaySceneManager instance;
    public static UIPlaySceneManager getInstance()
    {
        if (instance == null)
        {
            instance = new UIPlaySceneManager();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    [Header("--------------COMPONENT----------------")]
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject timeOutMenu;
    [SerializeField] public GameObject outOfSlotMenu;
    [SerializeField] public GameObject congratulateMenu;
    [SerializeField] public GameObject playLevelMenu;
    [SerializeField] public GameObject AddBuffMenu;
    [SerializeField] public GameObject PackMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
        timeOutMenu.SetActive(false);
        outOfSlotMenu.SetActive(false);
        congratulateMenu.SetActive(false);
        playLevelMenu.SetActive(false);
        AddBuffMenu.SetActive(false);
        PackMenu.SetActive(false);
        PackMenu.GetComponent<PackOpenMenu>().LoadStart();
    }

    // Back to main scene
    public void BackToMainScene()
    {
        SaveAndLoadManager.getInstance().SaveHUBData();
        SaveAndLoadManager.getInstance().SaveSoundData();
        SceneManager.LoadScene(0);
    }

    // pause menu

    public void OnSettingBtn()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        pauseMenu.SetActive(true);
        GameControler.getInstance().Pause();
    }

    //Time out Menu
    public void OpenTimeOutMenu()
    {
        timeOutMenu.SetActive(true);
        GameControler.getInstance().Pause();
    }
}
