using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAdditive : MonoBehaviour
{
    public bool LoadDeathMatch;
    public bool LoadDeathMatchTime;
    public bool LoadBattle;
    public bool LoadTitle;
    public bool Test;

    [Header("Title Close")]
    public GameObject MainCamera;
    public GameObject stageInfo;
    public GameObject Spawner;

    // Start is called before the first frame update
    void Start()
    {
        if (LoadTitle)
        {
            SceneManager.LoadScene("TrueMainMenu", LoadSceneMode.Additive);
            MainCamera.SetActive(false);
            stageInfo.SetActive(false);
            Spawner.SetActive(false);
        }

        if (!StageManager.Static_Testing) return;

        if (LoadBattle)
        {
            SceneManager.LoadScene("CrownScene", LoadSceneMode.Additive);
        }
        if (Test)
        {
            SceneManager.LoadScene("SceneForTest 1", LoadSceneMode.Additive);
        }
        if (LoadDeathMatch)
        {
            SceneManager.LoadScene("DeathMatchBrawl", LoadSceneMode.Additive);
        }
        if (LoadDeathMatchTime)
        {
            SceneManager.LoadScene("DeathMatchBrawlTime", LoadSceneMode.Additive);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
