using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Toggle toggleSound;
    [SerializeField] Toggle toggleMusic;
    // Start is called before the first frame update
    void Start()
    {
        LoadSetting(SaveAndLoadManager.getInstance().LoadSoundData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSetting(SettingSoundData data)
    {
        toggleMusic.isOn = data.isOnMusic;
        toggleSound.isOn = data.isOnSound;
    }

    public void OnClosePauseMenu()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        this.gameObject.SetActive(false);
        GameControler.getInstance().Resume();
    }

    public void OnBackHomeClick()
    {
        SoundManager.getInstance().PlaySound("ButtonClick");
        SceneManager.LoadScene(0);
    }

    public void OnChangeValueToggleSound()
    {
        SoundManager.getInstance().setOnOffSound(toggleSound.isOn);
    }

    public void OnChangeValueToggleMusic()
    {
        SoundManager.getInstance().setOnOffMusic(toggleMusic.isOn);
    }
}
