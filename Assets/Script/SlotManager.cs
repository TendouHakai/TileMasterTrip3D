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
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        n = 7;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getCurrentSlot()
    {
        if(index == n)
        {
            return Vector3.zero;
        }
        return slots[index++].position;
    }
}
