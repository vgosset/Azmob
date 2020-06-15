using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landscape : MonoBehaviour
{
    public List<GameObject> land_lst;

    private int id = -1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void NextLandscape()
    {
        if (id == land_lst.Count - 1)
        {
            id = 0;
        }
        else
            id ++;

        for (int i = 0; i < land_lst.Count; i++)
        {
            if (i == id)
                land_lst[i].SetActive(true);
            else
                land_lst[i].SetActive(false);
        }
    }
}
