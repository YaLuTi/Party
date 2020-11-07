using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFacility : FacilityArea
{
    SceneChangeTest changeTest;
    // Start is called before the first frame update
    void Start()
    {
        changeTest = GetComponent<SceneChangeTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse()
    {
        base.OnUse();
        changeTest.LoadScene();
    }
}
