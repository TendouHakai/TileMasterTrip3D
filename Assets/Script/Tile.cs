using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("---------ID-----------")]
    [SerializeField] public int ID;
    [Header("---------Component-----------")]
    [SerializeField] MeshRenderer render;
    [SerializeField] public Rigidbody body;
    [SerializeField] Outline outline;
    [Header("---------Component-----------")]
    [SerializeField] GameObject effectFrefabs;

    bool isFaceUp = false;
    bool isMove = false;
    float speed = 2f;
    float startTime = 0f;
    float distance = 0f;
    public bool isOnSlot = false;
    bool isToSlot = false;
    Vector3 startMarker;
    Vector3 endMarker;

    bool isRemove = false;

    bool isSelect = false;

    public bool isUpdateSlotManager = false;
    void Start()
    {
        TileConfig config = TileConfigs.getInstance().getConfig(ID);

        render.material.SetTexture("_MainTex", config.img);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFaceUp)
        {
            Quaternion vec = transform.rotation;
            vec.z = 0;
            vec.x = 0;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, vec, Time.time * 0.01f);
        }

        if(isMove)
        {
            Quaternion vec = transform.rotation;
            vec.y = 180;
            vec.z = 0;
            vec.x = 0;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, vec, Time.time * 0.01f);

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / distance;

            this.transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (Vector3.Distance(this.transform.position, endMarker) <= 0.01f)
            {
                if(isToSlot)
                {
                    isOnSlot = true;
                    isMove = false;
                    if (isUpdateSlotManager)
                    {
                        SlotManager.getInstance().deleteChain();
                        isUpdateSlotManager = false;
                    }
                }
                else
                {
                    isMove = false;
                }

            }
        }

        if (isOnSlot)
        {
            this.transform.position = endMarker;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if(isSelect)
        {
            Vector3 pos = this.transform.position;
            pos.y = -0.5f;
            this.transform.position = pos;
        }
    }

    IEnumerator StartFaceup()
    {
        isFaceUp = true;
        yield return new WaitForSeconds(3f);
        isFaceUp = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Background")
        {
            StartCoroutine(StartFaceup());
        }
    }

    public void MoveTo(Vector3 pos, float speed, bool isToSlot = true)
    {
        if (pos == Vector3.zero) return;
        this.speed = speed;
        startMarker = this.transform.position;
        endMarker = pos;
        startTime = Time.time;
        distance = Vector3.Distance(startMarker, endMarker);
        isMove = true;
        isOnSlot = false;
        this.isToSlot = isToSlot;
    }

    public void Select()
    {
        isSelect = true;
        outline.enabled = true;
    }

    public void Unselect()
    {
        isSelect = false;
        outline.enabled = false;
    }

    public bool IsMove()
    {
        return isMove;
    }

    public void remove()
    {
        isRemove = true;
        GameObject effect = Instantiate(effectFrefabs, this.transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        SpawnManager.getInstance().removeTile(this);
        Destroy(this.gameObject);
    }
}
