using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StarChestMenu : MonoBehaviour
{
    [Header("--------------SLIDER BUTTON-------------")]
    [SerializeField] Slider slider;
    [SerializeField] Image sliderColorImg;
    [SerializeField] TextMeshProUGUI textSlider;
    [Header("--------------SLIDER MENU-------------")]
    [SerializeField] Slider sliderMenu;
    [SerializeField] Image sliderMenuColorImg;
    [SerializeField] TextMeshProUGUI textSliderMenu;


    // color
    [SerializeField] Color colorFull;
    [SerializeField] Color colorNormal;

    public StarChestConfig currentConfig;
    public int startStar;
    public int currentStar;
    public int endStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void loadStart()
    {
        currentConfig = StarChestConfigs.getInstance().getIsNotClaimConfig();
        Debug.Log(currentConfig);
        if (currentConfig == null)
        {
            slider.value = 0f;
            textSlider.text = "No chest";
            return;
        }
        currentStar = HUBManger.getInstance().star;
        endStar = currentConfig.Star;
        startStar = StarChestConfigs.getInstance().getConfig(currentConfig.ID-1) != null ? StarChestConfigs.getInstance().getConfig(currentConfig.ID-1).Star : 0;

        slider.value = (float)(currentStar - startStar) / (endStar - startStar);
        if (slider.value == 1)
        {
            sliderColorImg.color = colorFull;
            textSlider.text = "Full";
        }
        else
        {
            sliderColorImg.color = colorNormal;
            textSlider.text = (currentStar - startStar).ToString() + "/" + (endStar - startStar).ToString();
        }
    }

    public void Claim()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        if (currentConfig == null)
        {
            
        }
        else if (HUBManger.getInstance().star >= endStar)
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

    private void OnEnable()
    {
        sliderMenu.value = slider.value;
        textSliderMenu.text = textSlider.text;
    }
}
