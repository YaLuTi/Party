using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScore : MonoBehaviour
{
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
    }
}
