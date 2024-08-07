
using System.Collections.Generic;
using UnityEngine;


public class SpawnCodes : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject[] items;
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
        
        for (int i = 0; i < items.Length; i++)
        {
            GameObject item = Instantiate(items[i], spawnPoints[randomField].transform.position,
                spawnPoints[randomField].transform.rotation);
            filled = true;
            if (filled)
            {
                spawnPoints.Remove(spawnPoints[randomField]);
            }
            randomField = Random.Range(0, spawnPoints.Count);
          
        }
       
    }
}
