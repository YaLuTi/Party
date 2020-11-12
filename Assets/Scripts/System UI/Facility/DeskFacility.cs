using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.InputSystem;

public class DeskFacility : FacilityArea
{
    List<DeskItem> deskItems = new List<DeskItem>();

    [Header("UI")]
    public GameObject UI;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI IntroText;

    [Header("Camera")]
    public GameObject Object;
    public Transform Location;
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

        foreach(Gamepad gamepad in Gamepad.all)
        {
            if (UI.activeSelf)
            {
                h = gamepad.rightStick.x.ReadValue();
                v = gamepad.rightStick.y.ReadValue();
                Object.transform.Rotate(new Vector3(0, 0, -h));
                Object.transform.Rotate(new Vector3(-v, 0, 0));
            }

            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                if (IsReady && !UI.activeSelf)
                {
                    UI.SetActive(true);

                    Object = Instantiate(deskItems[choosing].gameObject, Location);
                    Object.transform.localPosition = Vector3.zero;
                    Object.transform.localEulerAngles = deskItems[choosing].RotationOffset;

                    NameText.text = deskItems[choosing].ItemName;
                    IntroText.text = deskItems[choosing].ItemIntro;
                }
            }

            // Cancel
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                if (UI.activeSelf)
                {
                    UI.SetActive(false);
                    Destroy(Object);
                    return;
                }
                if (IsUsing && playableDirector.state != PlayState.Playing)
                {
                    playableDirector.Stop();
                    playBack.Play();

                    FacilityManager.UsingDirector = playBack;

                    StageManager.EnablePlayerControl();
                    deskItems[choosing].Cancel();
                    IsUsing = false;
                    IsReady = false;
                }
            }

            // DPad Choosing
            if(IsReady && !UI.activeSelf)
            {
                if (gamepad.dpad.left.wasPressedThisFrame)
                {
                    deskItems[choosing].Cancel();
                    choosing--;
                    if (choosing < 0) choosing = deskItems.Count - 1;
                    deskItems[choosing].Choose();
                }
                if (gamepad.dpad.right.wasPressedThisFrame)
                {
                    deskItems[choosing].Cancel();
                    choosing++;
                    if (choosing >= deskItems.Count) choosing = 0;
                    deskItems[choosing].Choose();
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

    public void Ready()
    {
        IsReady = true;
        choosing = 0;
        deskItems[choosing].Choose();
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
