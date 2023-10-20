using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    private static GameControler instance;
    public static GameControler getInstance()
    {
        if (instance == null)
        {
            instance = new GameControler();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    bool isTouch;
    public bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        isTouch = false;
    }

    RaycastHit raycastHit;

    // Update is called once per frame
    void Update()
    {
        if(!isPause)
        {
            if (Input.touchCount >= 1 && isTouch == false)
            {
                isTouch = true;
                OnTouch(Input.GetTouch(0).position);
            }

            if (Input.touchCount == 0 && isTouch == true)
            {
                isTouch = false;
                if (raycastHit.transform != null && raycastHit.transform.tag == "Tile")
                {
                    raycastHit.transform.GetComponent<Tile>().Unselect();
                }
            }
        }    
    }

    void OnTouch(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.tag == "Tile")
            {
                SoundManager.getInstance().PlaySound("PickTile");
                Tile tile = raycastHit.transform.GetComponent<Tile>();
                if (!tile.isOnSlot)
                {
                    tile.Select();
                    SlotManager.getInstance().addTile(tile);
                }
                
            }
        }

    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
}
