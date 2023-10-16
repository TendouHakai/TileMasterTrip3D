using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Cowboy : MonoBehaviour
{
    [Header("---------Spawn-----------")]
    public int n;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Tile Tilefrefabs;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawning()
    {
        for(int i =0; i<n; i++)
        {
            Tile tile = Instantiate(Tilefrefabs, SpawnPoint.position, Quaternion.identity);

            if (isStartCircular)
            {
                angle += 10;
                if (force < FORCE_MAX) force += 0.1f;

                Vector3 vec = Vector3.zero;
                vec.x = Mathf.Cos((angle + 90f) * Mathf.Deg2Rad);
                vec.z = Mathf.Sin((angle + 90f) * Mathf.Deg2Rad);
                tile.body.AddForce(vec * force, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(speedSpawn);
        }
        stopSpawn();
    }

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
        ani.Play("CowboyEndAni");
    }

    public void End()
    {
        wall.SetActive(false);
    }
}
