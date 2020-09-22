using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityItemSpawner : MonoBehaviour
{
    public GameObject SpawnItem;
    public GameObject Copy;

    // Start is called before the first frame update
    void Start()
    {
        Copy = Instantiate(SpawnItem);
    }

    // Update is called once per frame
    void Update()
    {
        if(Copy.transform.parent != null)
        {

        }
    }
}
