using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScore : MonoBehaviour
{
    [SerializeField]
    Vector3 spawnPosition;

    int cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            if(cooldown > 45)
            {
                cooldown = 0;
                int id = GetComponentInParent<PlayerIdentity>().PlayerID;
                ScoreManager.AddScore(id, 1);
            }
            cooldown++;
        }
        else
        {
            cooldown = 0;
        }

        if(transform.position.y < -5)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = spawnPosition;
        }
    }
}
