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
    public List<string> listTileID = new List<string>();

    [SerializeField] Cowboy cowboy;

    private void Start()
    {
        loadLevel(HUBManger.getInstance().getCurrentLevel());
    }

    public void loadLevel(int ID)
    {
        currentLevel = LevelConfigs.getInstance().getConfig(ID);
        if (currentLevel == null)
        {
            UIPlaySceneManager.getInstance().WinMenu.SetActive(true);
            GameControler.getInstance().Pause();
            return;
        }
        clear();
        // create list ID
        foreach (TileInLevel tile in currentLevel.tileInLevels)
        {
            for(int i =0; i<tile.chain; i++)
            {
                listTileID.Add(tile.IDTile);
                listTileID.Add(tile.IDTile);
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

    public bool IsSpawn()
    {
        //if (index < listTileID.Count) return true;
        if (listTileID.Count>0) return true;
        return false;
    }

    public string SpawnID()
    {
        int index = Random.RandomRange(0, listTileID.Count);
        string id = listTileID[index];
        listTileID.RemoveAt(index);
        return id;
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
            HUBManger.getInstance().addLevel(1);
            UIPlaySceneManager.getInstance().PackMenu.SetActive(true);
            //UIPlaySceneManager.getInstance().congratulateMenu.SetActive(true);
        }
    }

    // add tile to Slot
    public void addTileToSlot(string ID, int n)
    {
        if (ID == "") ID = tiles[0].ID;
        for(int i =0; i<tiles.Count; i++)
        {
            if (tiles[i].ID == ID || ID == "")
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
