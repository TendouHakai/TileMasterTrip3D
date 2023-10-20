using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelChestMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image sliderColorImg;
    [SerializeField] TextMeshProUGUI textSlider;
    [SerializeField] TextMeshProUGUI TextEndLevel;

    // color
    [SerializeField] Color colorFull;
    [SerializeField] Color colorNormal;

    public LevelChestConfig currentConfig;
    public int startLevel;
    public int currentLevel;
    public int endLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void loadStart()
    {
        currentConfig = LevelChestConfigs.getInstance().getIsNotClaimConfig();
        if(currentConfig == null)
        {
            slider.value = 0f;
            textSlider.text = "No chest";
            return;
        }
        currentLevel = HUBManger.getInstance().getCurrentLevel();
        endLevel = currentConfig.level;
        startLevel = LevelChestConfigs.getInstance().getPreviousConfigByLevel(endLevel) != null ? LevelChestConfigs.getInstance().getPreviousConfigByLevel(endLevel).level : 1;

        slider.value = (float)(currentLevel - startLevel) / (endLevel - startLevel);
        if (slider.value == 1)
        {
            sliderColorImg.color = colorFull;
            textSlider.text = "Full";
        }
        else
        {
            sliderColorImg.color = colorNormal;
            textSlider.text = (currentLevel - startLevel).ToString() + "/" + (endLevel - startLevel).ToString();

            TextEndLevel.text = "Pass " + endLevel.ToString() + " level to open"; 
        }
    }

    public void Claim()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (HUBManger.getInstance().level >= endLevel)
        {
            currentConfig.isClaim = true;
            UIMainSceneManager.getInstance().RewardMenu.SetActive(true);
            UIMainSceneManager.getInstance().RewardMenu.GetComponent<RewardMenu>().LoadRewards(currentConfig.rewards);
            loadStart();
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void OnContinueBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        this.gameObject.SetActive(false);
    }

    public void OnCloseBtnCLick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
