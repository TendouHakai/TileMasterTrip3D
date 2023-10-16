using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnRemoveBtnClick()
    {
        Debug.Log("run");
        if (SlotManager.getInstance().isCanRemove())
        {
            Debug.Log("remove");
            SlotManager.getInstance().Remove();
        }
    }

    public void OnGuideBtnClick()
    {
        SlotManager.getInstance().addGuide();
    }
}
