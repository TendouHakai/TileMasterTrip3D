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

    int star = 0;
    float time = 900;
    int level = 1;
    int combo = 0;
    // time combo
    float timeComboStart = 0f;
    float timeCombo = 5f;
    bool isCombo = false;
    [Header("---------Component-----------")]
    [SerializeField] TextMeshProUGUI starText;
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
        starText.text = star.ToString();
        TimeText.text = time.ToString();
        LevelText.text = "Level" + level.ToString();
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time-=Time.deltaTime;
            TimeText.text = getTimeText();
        }
        if(isCombo)
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

    string getTimeText()
    {
        int phut = (int)time / 60;
        int giay = (int)time % 60;
        return phut.ToString() +":" + giay.ToString();   
    }

    public void addStar(int n)
    {
        this.star += n;
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
}
