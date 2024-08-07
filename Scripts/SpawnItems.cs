using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [Range(1,10)]
    public int itemsCount;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject []  items;
    private int randomField;
    private bool filled;
 
    void Start()
    {
        randomField = Random.Range(0, spawnPoints.Count);
        Spawn();
    }

    // Update is called once per frame
    private void Spawn()
    {

        for (int i = 0; i < items.Length ; i++)
        {
            GameObject item =Instantiate(items[i], spawnPoints[randomField].transform.position,
                items[i].transform.rotation);
            item.name = items[i].name;
            item.transform.parent = spawnPoints[randomField].transform;
            if (item.transform.localScale != items[i].transform.localScale)
                item.transform.localScale = items[i].transform.localScale;
            
            filled = true;
            if (filled)
                spawnPoints.Remove(spawnPoints[randomField]);
            

            randomField = Random.Range(0, spawnPoints.Count);
        }
    }
}
