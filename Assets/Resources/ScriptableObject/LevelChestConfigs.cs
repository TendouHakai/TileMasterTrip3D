using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChestConfigs : MonoBehaviour
{
    private static SoundConfigs instance;
    public static SoundConfigs getInstance()
    {
        if (instance == null)
        {
            instance = Resources.Load<SoundConfigs>("ScriptableObject/SoundConfig");
        }
        return instance;
    }


}
