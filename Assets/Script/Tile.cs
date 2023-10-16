using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("---------ID-----------")]
    [SerializeField] int ID;
    [Header("---------Component-----------")]
    [SerializeField] MeshRenderer render;
    [SerializeField] public Rigidbody body;
    [SerializeField] Outline outline;
    
    bool isFaceUp = false;
    bool isMove = false;
    float startTime = 0f;
    float distance = 0f;
    bool isOnSlot = false;
    Vector3 destination;
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
            vec.y = 0;
            vec.z = 0;
            vec.x = 0;
            this.transform.rotation = Quaternion.Lerp(transform.rotation, vec, Time.time * 0.01f);

            float distCovered = (Time.time - startTime) * 2f;
            float fractionOfJourney = distCovered / distance;

            this.transform.position = Vector3.Lerp(transform.position, destination, fractionOfJourney);

            if (Vector3.Distance(this.transform.position, destination) <=0.1f)
            {
                isOnSlot = true;
                isMove = false;
            }
        }

        if (isOnSlot)
        {
            this.transform.position = destination;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
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

    public void MoveTo(Vector3 pos)
    {
        if (pos == Vector3.zero) return;
        destination = pos;
        startTime = Time.time;
        distance = Vector3.Distance(this.transform.position, destination);
        isMove = true;
    }
}
