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
    [SerializeField] GameObject addSlotBtn;
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
        if (index == -1)
        {
            if (tiles.Count == n)
            {
                UIPlaySceneManager.getInstance().outOfSlotMenu.SetActive(true);
            }
            return;
        }
        else if (index == -2) return;

        SoundManager.getInstance().PlaySound("SlotRecycle");
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
        int flag = 0;
        for (int i = 1; i < tiles.Count; i++)
        {
            if (tiles[index].isOnSlot == false || tiles[i].isOnSlot == false)
            {
                flag = 1;
                continue;
            }
            else if (tiles[index].ID == tiles[i].ID)
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

        return flag == 0? -1: -2;
    }

    public void addTile(Tile tile)
    {
        if (tiles.Count == n)
        {
            UIPlaySceneManager.getInstance().outOfSlotMenu.SetActive(true);
            return;
        }
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

    public void clear()
    {
        for(int i =0; i < tiles.Count; ++i)
        {
            tiles.RemoveAt(i);
            i--;
        }

        n = 7;
    }

    // remove tile

    public bool isCanRemove()
    {
        return tiles.Count != 0;
    }

    public void Remove(int n =1)
    {
        for(int i =0; i<n; i++)
        {
            tiles[tiles.Count - 1].MoveTo(new Vector3(Random.Range(-2.7f, 2.7f), 1f, Random.Range(-3.5f, 3.5f)), 20f, false);
            tiles.RemoveAt(tiles.Count - 1);
        }
    }

    // Add guide

    public bool isContain(Tile tile)
    {
        return tiles.Contains(tile);    
    }

    public void addGuide()
    {
        if(tiles.Count == 0)
        {
            SpawnManager.getInstance().addTileToSlot("", 3);
            return;
        }
        int index = 0;
        bool flag = false;

        for(int i =1; i<tiles.Count; ++i)
        {
            if (tiles[index].ID == tiles[i].ID)
            {
                flag = true;
                break;
            }
            else
            {
                index = i;
            }
        }

        if(flag == true)
        {
            SpawnManager.getInstance().addTileToSlot(tiles[index].ID, 1);
        }
        else
        {
            SpawnManager.getInstance().addTileToSlot(tiles[0].ID, 2);
        }
    }

    // Add slot
    public void addASlot()
    {
        n++;
    }

    // public reset
    public void resetSlot()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Destroy(tiles[i].gameObject);
            tiles.RemoveAt(i);
            i--;
        }
        tiles.Clear();
        addSlotBtn.SetActive(true);
        n = 7;
    }
}
