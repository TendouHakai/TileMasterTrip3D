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

    private void Start()
    {
        loadLevel(1);
    }

    public void loadLevel(int ID)
    {
        currentLevel = LevelConfigs.getInstance().getConfig(ID);
        Debug.Log(currentLevel);
        listTileID.Clear();
        // create list ID
        foreach (TileInLevel tile in currentLevel.tileInLevels)
        {
            for(int i =0; i<tile.count; i++)
            {
                listTileID.Add(tile.IDTile);
            }
        }
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
}
