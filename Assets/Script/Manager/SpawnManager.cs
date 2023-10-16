using System.Collections;
using System.Collections.Generic;
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
        loadLevel(1);
    }

    public void loadLevel(int ID)
    {
        currentLevel = LevelConfigs.getInstance().getConfig(ID);
        listTileID.Clear();
        // create list ID
        foreach (TileInLevel tile in currentLevel.tileInLevels)
        {
            for(int i =0; i<tile.count; i++)
            {
                listTileID.Add(tile.IDTile);
            }
        }

        cowboy.startAni();
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
            loadLevel(currentLevel.ID + 1);
        }
    }

    // add tile to Slot
    public void addTileToSlot(int ID, int n)
    {
        for(int i =0; i<tiles.Count; i++)
        {
            if (tiles[i].ID == ID)
            {
                if (!SlotManager.getInstance().isContain(tiles[i]))
                {
                    SlotManager.getInstance().addTile(tiles[i]);
                    n--;
                }
            }
            if (n <= 0) return;
        }
    }
}
