using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.InputSystem;

public class DeskFacility : FacilityArea
{
    List<DeskItem> deskItems = new List<DeskItem>();

    public GameObject UI;
    public GameObject Object;
    public GameObject Camera;

    public PlayableDirector Cine;
    public PlayableDirector playableDirector;
    public PlayableDirector playBack;

    float h;
    float v;

    public int choosing = 0;
    bool IsReady = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject desk = GameObject.FindGameObjectWithTag("DeskObject");
        for(int i = 0; i < desk.transform.childCount; i++)
        {
            deskItems.Add(desk.transform.GetChild(i).GetComponent<DeskItem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Object.transform.eulerAngles += new Vector3(0, -h, 0) * Time.deltaTime;
        Object.transform.eulerAngles += new Vector3(v, 0, 0) * Time.deltaTime;*/
        Object.transform.Rotate(new Vector3(0, 0, -h));
        Object.transform.Rotate(new Vector3(-v, 0, 0));

        foreach(Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                if (IsUsing && playableDirector.state != PlayState.Playing)
                {
                    playableDirector.Stop();
                    playBack.Play();
                    FacilityManager.UsingDirector = playBack;
                    StageManager.EnablePlayerControl();
                    IsUsing = false;
                }
            }

            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                if (IsUsing)
                {
                    UI.SetActive(true);
                }
            }
        }
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        if (!IsUsing && FacilityManager.UsingDirector.state != PlayState.Playing)
        {
            FacilityManager.UsingDirector.Stop();
            playableDirector.Play();

            IsUsing = true;
            StageManager.DisablePlayerControl();
        }
    }

    void OnRXAxis(InputValue value)
    {
        h = value.Get<float>();
    }
    void OnRYAxis(InputValue value)
    {
        v = value.Get<float>();
    }
}
