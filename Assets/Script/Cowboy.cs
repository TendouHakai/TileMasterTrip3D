using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Cowboy : MonoBehaviour
{
    [Header("---------Spawn-----------")]
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Tile Tilefrefabs;

    //int index;
    Coroutine SpawnCoroutine;
    public float speedSpawn = 0.01f;
    bool isStartCircular = false;
    float angle = 0f;
    float force = 2f;
    const float FORCE_MAX = 4f;

    [Header("---------Component-----------")]
    [SerializeField] Animator ani;
    [SerializeField] GameObject wall;

    void Start()
    {
        //index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawning()
    {
        Vector3 vec = Vector3.zero;
        SoundManager.getInstance().PlaySound("GameStart");
        while(SpawnManager.getInstance().IsSpawn())
        {
            Tile tile = Instantiate(Tilefrefabs, SpawnPoint.position + vec, Quaternion.identity);
            tile.ID = SpawnManager.getInstance().SpawnID();

            if (isStartCircular)
            {
                angle -= 10;
                if (force < FORCE_MAX) force += 0.1f;

                vec = Vector3.zero;
                vec.x = Mathf.Cos((angle + 90f) * Mathf.Deg2Rad);
                vec.z = Mathf.Sin((angle + 90f) * Mathf.Deg2Rad);
                vec.y = 0f;
                tile.body.AddForce(vec * force, ForceMode.Impulse);
            }

            SpawnManager.getInstance().addTile(tile);
            
            yield return new WaitForSeconds(speedSpawn);
        }
        stopSpawn();
    }

    // Next Level
    public void startAni()
    {
        ani.Play("CowboyBeginAni");
    }

    // event Animation

    public void startSpawn()
    {
        wall.SetActive(true);
        SpawnCoroutine = StartCoroutine(Spawning());
    }

    public void startCircular()
    {
        isStartCircular = true;
    }

    public void stopSpawn()
    {
        StopCoroutine(SpawnCoroutine);
        isStartCircular = false;
        //index = 0;
        ani.Play("CowboyEndAni");
        SoundManager.getInstance().StopSound();
    }

    public void End()
    {
        wall.SetActive(false);
    }
}
