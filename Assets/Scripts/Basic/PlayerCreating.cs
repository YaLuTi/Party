using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreating : MonoBehaviour
{
    public PlayerCraftingUI playerCreatingUI;
    PlayerInput playerInput;
    public List<GameObject> Hats = new List<GameObject>();
    public List<GameObject> RigHats = new List<GameObject>();
    int choosing = 0;
    bool IsEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Creat()
    {
        Hats[0].SetActive(true);
        RigHats[0].SetActive(true);
        IsEnable = true;
    }

    void OnUI_Right()
    {
        if (!this.enabled || !IsEnable) return;
        Hats[choosing].SetActive(false);
        RigHats[choosing].SetActive(false);
        choosing++;
        if(choosing >= Hats.Count)
        {
            choosing = 0;
        }
        Hats[choosing].SetActive(true);
        RigHats[choosing].SetActive(true);
        playerCreatingUI.Right(Hats[choosing].name);
    }
    void OnUI_Left()
    {
        if (!this.enabled || !IsEnable) return;
        Hats[choosing].SetActive(false);
        RigHats[choosing].SetActive(false);
        choosing--;
        if (choosing < 0)
        {
            choosing = Hats.Count - 1;
        }
        Hats[choosing].SetActive(true);
        RigHats[choosing].SetActive(true);
        playerCreatingUI.Left(Hats[choosing].name);
    }

    // 這寫法目前不可逆 要再修正
    void OnYes()
    {
        StageManager.LoadSceneCheck(); // 在增加玩家數字前檢查 才可以進到全員OK?畫面
        if (!this.enabled || !IsEnable) return;
        playerCreatingUI.Ready();
        StageManager.PlayerReady();

        playerInput.SwitchCurrentActionMap("GamePlay");

        this.enabled = false;
    }

    void OnNo()
    {

    }

    void OnPause()
    {
        StageManager.LoadSceneCheck();

        GameObject PausePanel = null;
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag("Pause"))
                {
                    PausePanel = objs[i].gameObject;
                    break;
                }
            }
        }
        if (PausePanel.activeSelf)
        {
            PausePanel.SetActive(false);
        }
        else
        {
            PausePanel.SetActive(true);
        }

        // StageManager.LoadSceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
