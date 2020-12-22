using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleFacility : FacilityArea
{
    SceneChangeTest changeTest;
    int ChoosingMode = 0;
    int ChoosingMap = 0;

    public GameObject UI;
    public GameObject HintUI;

    bool Using = false;

    public PlayableDirector playableDirector;

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
        if(PlayersNum > 0 && !Using)
        {
            HintUI.SetActive(true);
        }
        else
        {
            HintUI.SetActive(false);
        }
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        UI.SetActive(true);
        Using = true;
        StageManager.UIOn();
        playableDirector.Play();
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
