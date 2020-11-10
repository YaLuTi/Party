﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFacility : FacilityArea
{
    SceneChangeTest changeTest;
    public static int ChoosingMode = 1;

    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        changeTest = GetComponent<SceneChangeTest>();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        changeTest.LoadScene(1,1);
        meshRenderer.enabled = false;
    }
}