using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SceneChangeFacility : FacilityArea
{
    SceneChangeTest changeTest;
    int ChoosingMode = 0;
    int ChoosingMap = 0;
    
    public GameObject HintUI;
    public PlayableDirector playableDirector;

    bool Using = false;
    bool HintActive = false;

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
        if (PlayersNum > 0 && !Using)
        {
            HintUI.SetActive(true);
            HintActive = true;
        }
        else if (PlayersNum == 0 && HintActive)
        {
            HintUI.SetActive(false);
            HintActive = false;
        }
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        StartCoroutine(GOGO());
        // meshRenderer.enabled = false;
    }

    public void GO()
    {
        StartCoroutine(GOGO());
    }

    IEnumerator GOGO()
    {
        Debug.Log("CX");
        playableDirector.Play();
        yield return new WaitForSeconds(1);
        changeTest.LoadMiniGame("MiniGame");
        yield return null;
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
