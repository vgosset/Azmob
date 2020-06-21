using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyAllEnemies()
    {
        Invoke("DestroyEnemies", 1f);
    }
    public void DestroyEnemies()
    {
        var enemies = FindObjectsOfType<EnemieLife>();

        foreach (EnemieLife enemy in enemies)
        {
            enemy.Die();   
        }
    }
}
