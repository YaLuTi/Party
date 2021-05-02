using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField]
    bool Spawn = false;
    [SerializeField]
    GameObject Zombie;

    int i;

    float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Spawn && cooldown > 30)
        {
            Instantiate(Zombie, transform.position, transform.rotation);
            cooldown = 0;
            i++;
            Debug.Log(i);
        }
        cooldown++;
    }
}
