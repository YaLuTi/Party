using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject_Punch : MonoBehaviour
{
    // This Script is only for test. Used to trigger PunchObject. Not sure is there a better way to do.

    public PunchObject Punch;
    void Start()
    {
        Punch = GetComponentInChildren<PunchObject>();
    }
    
    void Update()
    {
        
    }

    void OnShoot()
    {
        Punch.Fire();
    }

}
