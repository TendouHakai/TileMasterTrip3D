using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager getInstance()
    {
        if (instance == null)
        {
            instance = new SpawnManager();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    LevelConfig currentLevel;
    List<Tile> tiles = new List<Tile>();
    List<int> listTileID = new List<int>();

    [SerializeField] Cowboy cowboy;

    private void Start()
    {
        loadLevel(HUBManger.getInstance().getCurrentLevel());
    }

    public void loadLevel(int ID)
    {
        currentLevel = LevelConfigs.getInstance().getConfig(ID);
        clear();
        // create list ID
        foreach (TileInLevel tile in currentLevel.tileInLevels)
        {
            for(int i =0; i<tile.count; i++)
            {
                listTileID.Add(tile.IDTile);
            }
        }

        //reset
        HUBManger.getInstance().starInLevel = 0;
        HUBManger.getInstance().startTime(currentLevel.Time);
        SlotManager.getInstance().resetSlot();  
        

        cowboy.startAni();
    }

    public void clear()
    {
        for(int i =0; i<tiles.Count; i++)
        {
            Destroy(tiles[i].gameObject);
            tiles.RemoveAt(i);
            i--;
        }

        listTileID.Clear();
    }

    public bool IsSpawn(int index)
    {
        if (index < listTileID.Count) return true;
        return false;
    }

    public int SpawnID(int index)
    {
        return listTileID[index];
    }
    
    public void addTile(Tile tile)
    {
        tiles.Add(tile);
    }

    public void removeTile(Tile tile)
    {
        tiles.Remove(tile);
        if(tiles.Count <= 0)
        {
            HUBManger.getInstance().level+=1;
            UIPlaySceneManager.getInstance().PackMenu.SetActive(true);
            //UIPlaySceneManager.getInstance().congratulateMenu.SetActive(true);
        }
    }

    // add tile to Slot
    public void addTileToSlot(int ID, int n)
    {
        for(int i =0; i<tiles.Count; i++)
        {
            if (tiles[i].ID == ID || ID ==-1)
            {
                if (!SlotManager.getInstance().isContain(tiles[i]))
                {
                    SlotManager.getInstance().addTile(tiles[i]);
                    --n;
                }
            }
            if (n <= 0) return;
        }
    }
}
