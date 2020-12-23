using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreating : MonoBehaviour
{
    public ProfileChooseUI profileChooseUI;
    PlayerInput playerInput;

    public PlayerIdentity playerIdentity;

    public ClothDataArray[] clothDataArrays;
    public Transform[] clothOffset; // 0 hat 1 face
    public Transform[] RigclothOffset;

    int[] ClothIDArray;
    GameObject[] ClothClone;
    GameObject[] RigClothClone;

    public int selecting = 0;

    bool IsEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerIdentity = transform.root.GetComponent<PlayerIdentity>();
    }

    public void Creat()
    {
        //
        StageManager.PlayerProfile[playerIdentity.PlayerID] = playerIdentity.PlayerID; // playerIdentity.PlayerID
        selecting = playerIdentity.PlayerID;

        if (profileChooseUI != null)
        {
            profileChooseUI.Set(playerIdentity.PlayerID);
        }

        ClothIDArray = new int[1]; // 手動設定吧
        ClothIDArray[0] = PlayerPrefs.GetInt("Profile_" + selecting + "_HAT");

        ClothClone = new GameObject[clothDataArrays.Length];
        RigClothClone = new GameObject[clothDataArrays.Length];
        ChangeHat();
        IsEnable = true;
    }

    void OnUI_Right()
    {
        if (!this.enabled || !IsEnable) return;
        selecting++;
        if(selecting >= PlayerPrefs.GetInt("Profile_Count"))
        {
            selecting = 0;
        }
        profileChooseUI.Right(selecting);

        ClothIDArray[0] = PlayerPrefs.GetInt("Profile_" + selecting + "_HAT");

        ChangeHat();
    }
    void OnUI_Left()
    {
        if (!this.enabled || !IsEnable) return;
        selecting--;
        if (selecting < 0)
        {
            selecting = PlayerPrefs.GetInt("Profile_Count") - 1;
        }
        profileChooseUI.Left(selecting);

        ClothIDArray[0] = PlayerPrefs.GetInt("Profile_" + selecting + "_HAT");

        ChangeHat();
    }

    void OnUI_Up()
    {
    }
    void OnUI_Down()
    {
    }

    void ChangeHat()
    {
        for(int choosingArray = 0; choosingArray < ClothIDArray.Length; choosingArray++)
        {
            if (ClothClone[choosingArray] != null)
            {
                Destroy(ClothClone[choosingArray]);
            }
            ClothClone[choosingArray] = Instantiate(clothDataArrays[choosingArray].clothDatas[ClothIDArray[choosingArray]].cloth);
            Vector3 v = ClothClone[choosingArray].transform.position;
            Quaternion q = ClothClone[choosingArray].transform.rotation;
            Vector3 s = ClothClone[choosingArray].transform.localScale;

            ClothClone[choosingArray].transform.parent = clothOffset[0];

            ClothClone[choosingArray].transform.localPosition = v;
            ClothClone[choosingArray].transform.localRotation = q;
            ClothClone[choosingArray].transform.localScale = s;
            ClothClone[choosingArray].GetComponent<MeshRenderer>().enabled = false;

            if (RigClothClone[choosingArray] != null)
            {
                Destroy(RigClothClone[choosingArray]);
            }
            Destroy(RigClothClone[choosingArray]);
            RigClothClone[choosingArray] = Instantiate(clothDataArrays[choosingArray].clothDatas[ClothIDArray[choosingArray]].cloth);
            v = RigClothClone[choosingArray].transform.position;
            q = RigClothClone[choosingArray].transform.rotation;
            s = RigClothClone[choosingArray].transform.localScale;

            RigClothClone[choosingArray].transform.parent = RigclothOffset[choosingArray];

            RigClothClone[choosingArray].transform.localPosition = v;
            RigClothClone[choosingArray].transform.localRotation = q;
            RigClothClone[choosingArray].transform.localScale = s;
        }
    }

    void ChangeProfile()
    {
        // ClothIDArray[0] = 
    }

    // 這寫法目前不可逆 要再修正
    void OnEnter()
    {
        // StageManager.LoadSceneCheck(); // 在增加玩家數字前檢查 才可以進到全員OK?畫面
        if (!this.enabled || !IsEnable) return;
        profileChooseUI.Ready();
        StageManager.PlayerReady();

        // playerInput.SwitchCurrentActionMap("GamePlay");

        this.enabled = false;
    }

    void OnNo()
    {

    }

    public void ChangeInput(string state)
    {
        playerInput.SwitchCurrentActionMap(state);
    }

    void OnYes()
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

        /*if (PausePanel.activeSelf)
        {
            PausePanel.SetActive(false);
        }
        else
        {
            PausePanel.SetActive(true);
        }*/

        // StageManager.LoadSceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
