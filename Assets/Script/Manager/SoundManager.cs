using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager getInstance()
    {
        if (instance == null)
        {
            instance = new SoundManager();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SoundSource;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("Music");
        LoadData(SaveAndLoadManager.getInstance().LoadSoundData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Save and load

    public void LoadData(SettingSoundData data)
    {
        if (data != null)
        {
            MusicSource.volume = data.isOnMusic ? 1 : 0;
            SoundSource.volume = data.isOnSound ? 1 : 0;
        }
    }

    public void SaveData()
    {
        SaveAndLoadManager.getInstance().SaveSoundData();
    }

    // function

    public void PlayMusic(string ID)
    {
        SoundConfig config = SoundConfigs.getInstance().getConfig(ID);
        if (config != null)
        {
            MusicSource.clip = config.clip;
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }

    public void PlaySound(string ID)
    {
        SoundConfig config = SoundConfigs.getInstance().getConfig(ID);
        if (config != null)
        {
            SoundSource.clip = config.clip;
            SoundSource.Play();
        }
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public void StopSound()
    {
        SoundSource.Stop();
    }

    public void setOnOffMusic(bool isOn)
    {
        MusicSource.volume = isOn ? 1 : 0;
    }

    public bool getOnOffMusic()
    {
        return MusicSource.volume > 0;
    }

    public void setOnOffSound(bool isOn)
    {
        SoundSource.volume = isOn ? 1 : 0;
    }

    public bool getOnOffSound()
    {
        return SoundSource.volume > 0;
    }
}
