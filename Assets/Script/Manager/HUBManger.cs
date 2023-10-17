using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUBManger : MonoBehaviour
{
    private static HUBManger instance;
    public static HUBManger getInstance()
    {
        if (instance == null)
        {
            instance = new HUBManger();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    [Header("--------------INFO HUB----------------")]
    public bool isplayScene = false;

    [SerializeField] public int enegy = 0;
    [SerializeField] public int star = 0;
    [SerializeField] public int starInLevel = 0;
    [SerializeField] public int coin = 0;
    [SerializeField] public float time = 900;
    [SerializeField] public float TIME_MAX = 900;
    [SerializeField] public int level = 1;
    [SerializeField] int combo = 0;
    // time combo
    float timeComboStart = 0f;
    float timeCombo = 5f;
    bool isCombo = false;

    // time count
    bool isTiming = true;
    [Header("---------Component-----------")]
    [SerializeField] TextMeshProUGUI enegyText;
    [SerializeField] TextMeshProUGUI starText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI ComboText;

    [Header("---------Frefabs-----------")]
    [SerializeField] Star starFrefabs;
    [SerializeField] RectTransform target;

    // Start is called before the first frame update
    void Start()
    {
        LoadData(SaveAndLoadManager.getInstance().LoadHUBData());
        if (isplayScene)
        {
            starText.text = star.ToString();
            TimeText.text = time.ToString();
            LevelText.text = "Level" + level.ToString();
            slider.gameObject.SetActive(false);

            TIME_MAX = time;
        }
        else
        {
            enegyText.text = enegy.ToString();
            starText.text = star.ToString();
            coinText.text = coin.ToString();
            LevelText.text = level.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isplayScene)
        {
            if(isTiming)
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    TimeText.text = getTimeText();
                }
                else
                {
                    isTiming = false;
                    UIPlaySceneManager.getInstance().OpenTimeOutMenu();
                }
            }
            
            if (isCombo)
            {
                if (timeComboStart > timeCombo)
                {
                    combo = 0;
                    slider.gameObject.SetActive(false);
                    isCombo = false;
                }
                else
                {
                    slider.value = (timeCombo - timeComboStart) / timeCombo;
                    timeComboStart += Time.deltaTime;
                }
            }
        }
        else
        {

        }
    }

    // Save and load data
    public void LoadData(HUBData data)
    {
        if (data != null)
        {
            enegy = data.Enegy;
            star = data.Star;
            coin = data.Coin;
            level = data.Level;
        }
    }

    public void SaveData()
    {
        SaveAndLoadManager.getInstance().SaveHUBData();
    }

    // function

    string getTimeText()
    {
        int phut = (int)time / 60;
        int giay = (int)time % 60;
        return phut.ToString() +":" + giay.ToString();   
    }

    public void addStar(int n)
    {
        this.star += n;
        this.starInLevel += n;
        starText.text = this.star.ToString();
    }

    public void createStar(int index)
    {
        if(combo != 0)
        {
            timeComboStart = 0f;
            StartCoroutine(createStarWithCombo(index));
        }
        else
        {
            isCombo = true;
            timeComboStart = 0f;
            slider.gameObject.SetActive(true);
            Star star = Instantiate(starFrefabs);
            star.target = target;
            star.slot = index;
            star.transform.SetParent(this.transform);

        }
        combo++;
        ComboText.text = "Combo" + combo.ToString();  
        starText.text = this.star.ToString();
    }

    IEnumerator createStarWithCombo(int index)
    {
        for (int i = 0; i < combo; i++)
        {
            Star star = Instantiate(starFrefabs);
            star.target = target;
            star.slot = index;
            star.transform.SetParent(this.transform);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public int getCombo()
    {
        return combo;
    }

    public void addTime()
    {
        time += 5 * 60;
        isTiming = true;
    }

    public void startTime()
    {
        time = TIME_MAX;
        isTiming = true;
    }
}
