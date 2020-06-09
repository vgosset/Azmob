using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemie;

    [SerializeField] private List<Vector3> spawnPos;

    [SerializeField] private float spawnRate;

    void Start()
    {
        Spawn();
        StartCoroutine("StartEnemieSpawnLoop");
    }

    public IEnumerator StartEnemieSpawnLoop()
     {
         while(true)
         {
             yield return new WaitForSeconds(spawnRate);
             
             Spawn();
         }
     }
     private void Spawn()
     {
        for (int i = 0; i < spawnPos.Count; i++)
        {
            Instantiate(Enemie, spawnPos[i], Quaternion.identity);
        }
     }
}


