using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Playables;

public class MiniGameTutorial : MonoBehaviour
{
    KungFuPlayerControll player;
    int level = 0;

    PlayableDirector TutorialTimeline;

    public bool[] JumpChecker;
    bool[] SwingChecker;

    public Animator[] JumpButtonAnimator;
    public Animator[] SwingButtonAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        /*if (KungFuManager.IsFirst)
        {
            TutorialTimeline.Play();
            JumpChecker = new bool[StageManager.players.Count];
            SwingChecker = new bool[StageManager.players.Count];

            foreach(GameObject g in KungFuManager.players)
            {
                g.GetComponent<KungFuPlayerControll>().OnJump += JumpCheck;
                g.GetComponent<KungFuPlayerControll>().OnSwing += SwingCheck;
            }
        }*/
        TutorialTimeline = GetComponent<PlayableDirector>();
        if (KungFuManager.IsFirst)
        {
            TutorialTimeline.Play();
        }
    }

    void TestSet()
    {
        TutorialTimeline.Play();
        JumpChecker = new bool[StageManager.players.Count];
        SwingChecker = new bool[StageManager.players.Count];

        foreach (GameObject g in KungFuManager.players)
        {
            g.GetComponent<KungFuPlayerControll>().OnJump += JumpCheck;
            g.GetComponent<KungFuPlayerControll>().OnSwing += SwingCheck;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        bool gamepadPressed = false;
        if (gamepad == null)
        {
            gamepadPressed = false;
        }
        else
        {
            gamepadPressed = gamepad.startButton.IsPressed(0);
        }

        if ((gamepadPressed || Keyboard.current.anyKey.IsPressed(0)) && !FacilityManager.IsMenu)
        {
            TestSet();
        }

        switch (level)
        {
            case 0:
                break;
            case 1:
                if (JumpChecker.All(x => x))
                {
                    if(TutorialTimeline.state == PlayState.Paused)
                    {
                        TutorialTimeline.Play();
                    }
                    for (int i = 0; i < KungFuManager.players.Count; i++)
                    {
                        JumpButtonAnimator[i].SetTrigger("End");
                    }
                }
                break;
            case 2:
                if (SwingChecker.All(x => x))
                {
                    if (TutorialTimeline.state == PlayState.Paused)
                    {
                        TutorialTimeline.Play();
                    }
                    for (int i = 0; i < KungFuManager.players.Count; i++)
                    {
                        SwingButtonAnimator[i].SetTrigger("End");
                    }
                    level++;
                }
                break;
            default:
                break;
        }
    }

    public void JumpStart()
    {
        level = 1;
        for (int i = 0; i < KungFuManager.players.Count; i++)
        {
            JumpButtonAnimator[i].SetTrigger("Start");
        }
    }

    public void SwingStart()
    {
        level = 2;
        for (int i = 0; i < KungFuManager.players.Count; i++)
        {
            SwingButtonAnimator[i].SetTrigger("Start");
        }
    }

    void JumpCheck(int num)
    {
        if (level == 1)
        {
            JumpChecker[num] = true;
            JumpButtonAnimator[num].SetTrigger("Play");
        }
    }

    void SwingCheck(int num)
    {
        if (level == 2)
        {
            SwingChecker[num] = true;
            SwingButtonAnimator[num].SetTrigger("Play");
        }
    }
}
