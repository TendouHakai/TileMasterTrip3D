using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackOpenMenu : MonoBehaviour
{
    [Header("-------------PROCESS MENU-----------")]
    [SerializeField] GameObject ProcessMenu;
    
    [SerializeField] Slider slider;
    [SerializeField] float timeSlider;
    [SerializeField] TextMeshProUGUI textPercent;

    public int startLevel;
    public int currentLevel;
    public int endLevel;

    [Header("-------------CLAIM PACK MENU-----------")]
    [SerializeField] GameObject ClaimPackMenu;
    [SerializeField] GameObject Display;

    [SerializeField] List<Tile> TilesOpen;

    public void LoadStart()
    {
        currentLevel = HUBManger.getInstance().getCurrentLevel();
        startLevel = PackConfigs.getInstance().getPreviousConfigByLevel(currentLevel) != null ? PackConfigs.getInstance().getPreviousConfigByLevel(currentLevel).Level : 1;
        endLevel = PackConfigs.getInstance().getNextConfigByLevel(currentLevel) != null ? PackConfigs.getInstance().getNextConfigByLevel(currentLevel).Level : int.MaxValue;

        slider.value = (float)(currentLevel - startLevel) / (endLevel - startLevel);
    }

    private void Update()
    {
        
    }

    IEnumerator Process()
    {
        float speed = (0.01f*timeSlider)/ ((float)1 / (endLevel - startLevel));
        while (slider.value < (float)(currentLevel - startLevel) / (endLevel - startLevel))
        {
            slider.value += 0.01f;
            textPercent.text = ((int)(slider.value*100)).ToString() + "%";
            yield return new WaitForSeconds(speed);
        }

        yield return new WaitForSeconds(0.5f);

        ProcessMenu.SetActive(false);
        if (currentLevel == endLevel) LoadNewPack();
        else
        {
            this.gameObject.SetActive(false);
            UIPlaySceneManager.getInstance().congratulateMenu.SetActive(true);
        }
    }

    public void LoadNewPack()
    {
        slider.value = 0f;
        startLevel = PackConfigs.getInstance().getPreviousConfigByLevel(currentLevel) != null ? PackConfigs.getInstance().getPreviousConfigByLevel(currentLevel).Level : 1;
        endLevel = PackConfigs.getInstance().getNextConfigByLevel(currentLevel) != null ? PackConfigs.getInstance().getNextConfigByLevel(currentLevel).Level : int.MaxValue;

        ClaimPackMenu.SetActive(true);
        Display.SetActive(true);
        PackConfig config = PackConfigs.getInstance().getConfigByLevel(currentLevel);  
        for(int i = 0; i < config.TileOpens.Count; i++)
        {
            TilesOpen[i].gameObject.SetActive(true);
            TilesOpen[i].ID = config.TileOpens[i];
        }
    }

    public void OnClaimBtnClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        ClaimPackMenu.SetActive(false);
        // clear display
        for(int i =0; i<TilesOpen.Count; i++)
        {
            TilesOpen[i].gameObject.SetActive(false);
        }
        Display.SetActive(false);
        UIPlaySceneManager.getInstance().congratulateMenu.SetActive(true); 
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameControler.getInstance().Pause();
        ProcessMenu.SetActive(false);
        ClaimPackMenu.SetActive(false);

        currentLevel = HUBManger.getInstance().level;

        if (endLevel != int.MaxValue)
        {
            ProcessMenu.SetActive(true);
            StartCoroutine(Process());
        }
        else
        {
            UIPlaySceneManager.getInstance().congratulateMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
