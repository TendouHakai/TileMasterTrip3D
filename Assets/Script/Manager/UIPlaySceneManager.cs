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
    [SerializeField] public GameObject AddASlotMenu;
    [SerializeField] public GameObject PackMenu;


    void Start()
    {
        pauseMenu.SetActive(false);
        timeOutMenu.SetActive(false);
        outOfSlotMenu.SetActive(false);
        congratulateMenu.SetActive(false);
        playLevelMenu.SetActive(false);
        AddASlotMenu.SetActive(false);
        PackMenu.SetActive(false);
        PackMenu.GetComponent<PackOpenMenu>().LoadStart();
    }

    public void OnRemoveBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (SlotManager.getInstance().isCanRemove())
        {
            Debug.Log("remove");
            SlotManager.getInstance().Remove();
        }
    }

    public void OnGuideBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        SlotManager.getInstance().addGuide();
    }

    public void OnAddASlotBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        AddASlotMenu.SetActive(true);
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
