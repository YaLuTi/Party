using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreating : MonoBehaviour
{
    public PlayerCraftingUI playerCreatingUI;
    public List<GameObject> Hats = new List<GameObject>();
    public List<GameObject> RigHats = new List<GameObject>();
    int choosing = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Creat()
    {
        Hats[0].SetActive(true);
        RigHats[0].SetActive(true);
    }

    void OnUI_Right()
    {
        if (!this.enabled) return;
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
        if (!this.enabled) return;
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
    void OnShoot()
    {
        if (!this.enabled) return;
        StageManager.PlayerReady();
        this.enabled = false;
    }

    void OnPause()
    {
        StageManager.LoadSceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
