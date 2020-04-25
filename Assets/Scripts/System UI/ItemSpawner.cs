using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    [SerializeField]
    float zOffset;
    [SerializeField]
    float xMinRange;
    [SerializeField]
    float xMaxRange;
    [SerializeField]
    float zMinRange;
    [SerializeField]
    float zMaxRange;

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
            float x = Random.Range(xMinRange, xMaxRange);
            float z = Random.Range(zMinRange, zMaxRange);
            Vector3 p = new Vector3(xOffset + x, yOffset, zOffset + z);
            Instantiate(SpawnObject[0], p, Quaternion.identity);
        }
        CooldownCount++;
    }
}
