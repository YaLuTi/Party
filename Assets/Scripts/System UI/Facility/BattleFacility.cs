﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFacility : FacilityArea
{
    SceneChangeTest changeTest;
    int ChoosingMode = 0;
    int ChoosingMap = 0;

    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        changeTest = GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>();

        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        // meshRenderer.enabled = false;
    }

    public void GO()
    {
        changeTest.LoadScene(ChoosingMode, ChoosingMap);
    }

    public void SetMode(int i)
    {
        ChoosingMode = i;
    }

    public void SetMap(int i)
    {
        ChoosingMap = i;
    }
}
