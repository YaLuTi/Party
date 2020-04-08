using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] SpawnObject;
    
    float CooldownCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CooldownCount > 120)
        {
            CooldownCount = 0;
            float x = Random.Range(-10, 10);
            float y = Random.Range(-2, -14);
            Vector3 p = new Vector3(x, 10, y);
            Instantiate(SpawnObject[0], p, Quaternion.identity);
        }
        CooldownCount++;
    }
}
