using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreating : MonoBehaviour
{
    public PlayerCraftingUI playerCreatingUI;
    PlayerInput playerInput;

    public ClothDataArray[] clothDataArrays;
    public Transform[] clothOffset; // 0 hat 1 face
    public Transform[] RigclothOffset;

    int[] ClothArray;
    GameObject[] ClothClone;
    GameObject[] RigClothClone;
    int choosingArray = 0;

    bool IsEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Creat()
    {
        //
        ClothArray = new int[clothDataArrays.Length];

        
        ClothClone = new GameObject[clothDataArrays.Length];
        
        ClothClone[0] = Instantiate(clothDataArrays[0].clothDatas[0].cloth);
        ClothClone[0].transform.parent = clothOffset[0];
        ClothClone[0].transform.localScale = clothDataArrays[0].clothDatas[0].ScaleOffset;
        ClothClone[0].transform.localPosition = clothDataArrays[0].clothDatas[0].PositionOffset;
        ClothClone[0].transform.localRotation = Quaternion.Euler(clothDataArrays[0].clothDatas[0].RotationOffset);
        
        RigClothClone = new GameObject[clothDataArrays.Length];

        RigClothClone[choosingArray] = Instantiate(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].cloth);
        RigClothClone[choosingArray].transform.parent = RigclothOffset[choosingArray];
        RigClothClone[choosingArray].transform.localScale = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].ScaleOffset;
        RigClothClone[choosingArray].transform.localPosition = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].PositionOffset;
        RigClothClone[choosingArray].transform.localRotation = Quaternion.Euler(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].RotationOffset);

        IsEnable = true;
    }

    void OnUI_Right()
    {
        if (!this.enabled || !IsEnable) return;
        ClothArray[choosingArray]++;
        if(ClothArray[choosingArray] >= clothDataArrays[choosingArray].clothDatas.Length)
        {
            ClothArray[choosingArray] = 0;
        }
        playerCreatingUI.Right(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].name);

        ChangeHat();
    }

    void OnUI_Left()
    {
        if (!this.enabled || !IsEnable) return;
        ClothArray[choosingArray]--;
        if (ClothArray[choosingArray] < 0)
        {
            ClothArray[choosingArray] = clothDataArrays[choosingArray].clothDatas.Length - 1;
        }
        playerCreatingUI.Left(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].name);

        ChangeHat();
    }

    void ChangeHat()
    {
        Destroy(ClothClone[choosingArray]);
        ClothClone[choosingArray] = Instantiate(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].cloth);
        ClothClone[choosingArray].transform.parent = clothOffset[choosingArray];
        ClothClone[choosingArray].transform.localScale = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].ScaleOffset;
        ClothClone[choosingArray].transform.localPosition = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].PositionOffset;
        ClothClone[choosingArray].transform.localRotation = Quaternion.Euler(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].RotationOffset);

        Destroy(RigClothClone[choosingArray]);
        RigClothClone[choosingArray] = Instantiate(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].cloth);
        RigClothClone[choosingArray].transform.parent = RigclothOffset[choosingArray];
        RigClothClone[choosingArray].transform.localScale = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].ScaleOffset;
        RigClothClone[choosingArray].transform.localPosition = clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].PositionOffset;
        RigClothClone[choosingArray].transform.localRotation = Quaternion.Euler(clothDataArrays[choosingArray].clothDatas[ClothArray[choosingArray]].RotationOffset);
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
