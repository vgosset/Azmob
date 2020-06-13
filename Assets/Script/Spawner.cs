using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemie;

    [SerializeField] private List<Transform> spawnPos;

    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnMulti;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = 3f;
    }

    private void Update()
    {
        if (spawnTimer <=0)
        {
            Spawn();
        }
        UiManager.Instance.UpdateSpawnTimer(spawnTimer, spawnRate);
        spawnTimer -= Time.deltaTime;
    }
     
     private void Spawn()
     {
        UiManager.Instance.SpawnEffect();
        
        for (int i = 0; i < spawnPos.Count; i++)
        {
            Vector3 pos = new Vector3(spawnPos[i].position.x, -4.3f, 0);

            Instantiate(Enemie, pos, Quaternion.identity);
        }

        spawnRate *= spawnMulti;
        spawnTimer = spawnRate;
     }
}


