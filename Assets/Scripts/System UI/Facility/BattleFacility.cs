using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;


public class BattleFacility : FacilityArea
{
    SceneChangeTest changeTest;
    int ChoosingMode = 0;
    int ChoosingMap = 0;

    public GameObject UI;
    public GameObject UI2;
    public GameObject UI3;
    public GoodEventSystem eventSystem;
    public Button button;

    public GameObject HintUI;

    bool Using = false;
    bool HintActive = false;

    public PlayableDirector playableDirector;
    public PlayableDirector ExitDirector;

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
            HintActive = true;
        }
        else if(PlayersNum == 0 && HintActive)
        {
            HintUI.SetActive(false);
            HintActive = false;
        }

        var gamepad = Gamepad.current;
        bool gamepadPressed = false;
        if (gamepad == null)
        {
            gamepadPressed = false;
        }
        else
        {
            gamepadPressed = gamepad.buttonEast.isPressed;
        }

        if ((gamepadPressed || Keyboard.current.kKey.IsPressed(0)) && !FacilityManager.IsMenu && Using)
        {
            OnCancel();
        }
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        UI.SetActive(true);
        Using = true;
        StageManager.UIOn();
        // eventSystem.Select(button);
        playableDirector.time = 0;
        ExitDirector.Stop();
        playableDirector.Play();
        // meshRenderer.enabled = false;
    }

    public override void OnCancel()
    {
        base.OnCancel();
        UI.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
        Using = false;
        StageManager.EnablePlayerControl();
        ExitDirector.time = 0;
        playableDirector.Stop();
        ExitDirector.Play();
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
