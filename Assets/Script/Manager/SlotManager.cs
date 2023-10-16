using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    private static SlotManager instance;
    public static SlotManager getInstance()
    {
        if (instance == null)
        {
            instance = new SlotManager();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] List<Transform> slots = new List<Transform>();
    int n;

    List<Tile> tiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        n = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateTilePosition(int index, bool isAdd = true)
    {
        Debug.Log(tiles.Count);
        if(isAdd)
        {
            for (int i = tiles.Count - 1; i > index; i--)
            {
                tiles[i].MoveTo(slots[i].position, 20f);
            }
            tiles[index].MoveTo(slots[index].position, 20f);
        }
        else
        {
            for (int i = index; i < tiles.Count; i++)
            {
                tiles[i].MoveTo(slots[i].position, 10f);
            }
        }
    }

    public void deleteChain()
    {
        int index = checkChain();
        if (index == -1) return;
        for (int i = 0; i < 3; i++)
        {
            tiles[index].remove();
            tiles.RemoveAt(index);
        }
        HUBManger.getInstance().createStar(index);
        updateTilePosition(index, false);
    }

    public int checkChain()
    {
        if(tiles.Count == 0) return -1;
        int index = 0;
        int count = 1;
        for (int i = 1; i < tiles.Count; i++)
        {
            if (tiles[index].ID == tiles[i].ID)
            {
                count++;
                if (count == 3) return index;
            }
            else
            {
                index = i;
                count = 1;
            }
        }

        return -1;
    }



    public void addTile(Tile tile)
    {
        if (tiles.Count == n) return;
        int i = 0;
        tile.isUpdateSlotManager = true;
        for(; i<tiles.Count; i++)
        {
            if (tiles[i].ID == tile.ID)
            {
                tiles.Insert(i, tile);
                updateTilePosition(i);
                return;
            }
        }

        tiles.Add(tile);
        updateTilePosition(i);
    }
}
