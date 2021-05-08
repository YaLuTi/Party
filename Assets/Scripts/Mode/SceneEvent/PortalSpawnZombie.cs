using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnZombie : MonoBehaviour
{
    [SerializeField]
    GameObject Zombie;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Zombie, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
