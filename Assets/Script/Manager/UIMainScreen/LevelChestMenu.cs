using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelChestMenu : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image sliderColorImg;
    [SerializeField] TextMeshProUGUI textSlider;

    // color
    [SerializeField] Color colorFull;
    [SerializeField] Color colorNormal;

    LevelChestConfig currentConfig;
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
        currentLevel = HUBManger.getInstance().getCurrentLevel();
        endLevel = currentConfig != null ? currentConfig.level : int.MaxValue;
        startLevel = LevelChestConfigs.getInstance().getPreviousConfigByLevel(endLevel) != null ? LevelChestConfigs.getInstance().getPreviousConfigByLevel(endLevel).level : 0;
        
        if (endLevel != int.MaxValue)
        {
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
            }
        }
        else
        {
            slider.value = 0f;
            textSlider.text = "No chest";
        }
    }

    public void Claim()
    {
        if(slider.value == 1)
        {
            currentConfig.isClaim = true;
            loadStart();
            UIMainSceneManager.getInstance().RewardMenu.SetActive(true);
            UIMainSceneManager.getInstance().RewardMenu.GetComponent<RewardMenu>().LoadRewards(currentConfig.rewards);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
