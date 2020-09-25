using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityeItemSpawner : MonoBehaviour
{
    public GameObject SpawnItem;
    public GameObject Copy;

    // Start is called before the first frame update
    void Start()
    {
        Copy = Instantiate(SpawnItem, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(Copy.transform.parent != null)
        {
            Copy = Instantiate(SpawnItem, transform.position, transform.rotation);
        }
    }
}
