using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Playables;
using Cinemachine;

public delegate void OnBattleScene();

public class StageManager : MonoBehaviour
{
    public OnBattleScene OnBattleScene;
    static StageManager instance;
    public static List<GameObject> players = new List<GameObject>();
    public static int[] playerScore;
    PlayerInputManager inputManager;

    public static int[] scores; // 暫時弄成玩家名次

    static int PlayerReadyNum = 0;

    static bool TriggerLoadScene = false;
    static bool TriggerLoadEnd = false;

    public bool Testing = false;

    CinemachineTargetGroup targetGroup;
    public PlayableDirector EndDirector;
    public GameObject PlayerCraftUI;
    List<GameObject> PlayerCraftUIList = new List<GameObject>();
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();

        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this.gameObject);
        }
        else if (this == instance)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Instantiate(players[i]);
            }
        }

        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        if (Testing)GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
        // SceneManager.LoadScene("CharacterChoose", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (TriggerLoadScene)
        {
            TriggerLoadScene = false;
            EndDirector.Play();
        }
        if (TriggerLoadEnd)
        {
            TriggerLoadEnd = false;
            LoadEndScene();
        }
    }

    public void JoinEnable()
    {
        inputManager.EnableJoining();
    }

    public static void SetEndScene()
    {
        // for(int i = 0)
    }

    // Player Stop Start
    public static void StageStop()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputCancel();
        }
    }

    public void StageStart()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputEnable();
        }
    }
       
    // Title UI
    public static void PlayerReady()
    {
        PlayerReadyNum++;
    }

    // 這邊一團亂
    public static void LoadSceneCheck()
    {
        if(PlayerReadyNum >= players.Count)
        {
            playerScore = new int[players.Count];
            scores = new int[players.Count];
            TriggerLoadScene = true;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (Testing)
        {
            playerInput.transform.root.GetComponent<PlayerIdentity>().InputEnable();
            playerInput.SwitchCurrentActionMap("GamePlay");
            Destroy(playerInput.transform.root.GetComponentInChildren<PlayerCreating>());
            players.Add((playerInput.transform.root.gameObject));
            targetGroup.AddMember(playerInput.transform, 1, 0);
            LoadTestScene();
            // playerInput.transform.root.GetComponent<PlayerIdentity>().SetRagData();
        }
        else
        {
            DontDestroyOnLoad(playerInput.transform.root.gameObject);
            players.Add((playerInput.transform.root.gameObject));
            GameObject g = Instantiate(PlayerCraftUI);
            g.GetComponent<Transform>().localPosition = new Vector3(-1.15f + (players.Count - 1) * 2.25f, 3.11f, -0.6f);
            players[players.Count - 1].GetComponentInChildren<PlayerCreating>().playerCreatingUI = g.GetComponent<PlayerCraftingUI>();
            OnBattleScene += players[players.Count - 1].GetComponent<PlayerIdentity>().OnTest;
            PlayerCraftUIList.Add(g);
        }
    }

    public void PlayBGM()
    {
        Debug.Log("a");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
    }

    // Load to end scene
    public static void LoadEnd()
    {
        TriggerLoadEnd = true;
    }

    public void LoadEndScene()
    {
        StartCoroutine(_LoadEndScene());
    }

    IEnumerator _LoadEndScene()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().SetToAnimationMode();
        }
        yield return new WaitForSeconds(1.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return new WaitForFixedUpdate();

        StageInfo stageInfo;
        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();

        Debug.Log(players.Count);
        for (int i = 0; i < players.Count; i++)
        {
            if (i == 0)
            {
                players[scores[i]].GetComponent<PlayerIdentity>().SetToPosition(stageInfo.SpawnPosition[0]);
                players[scores[i]].GetComponent<PlayerIdentity>().SetToRotation(stageInfo.SpawnRotation[0]);
                players[scores[i]].GetComponent<PlayerIdentity>().SetKing();
                players[scores[i]].GetComponentInChildren<Animator>().SetTrigger("Sit");
            }
            else if (i == players.Count - 1)
            {
                players[scores[i]].GetComponent<PlayerIdentity>().SetToPosition(stageInfo.SpawnPosition[3]);
                players[scores[i]].GetComponent<PlayerIdentity>().SetToRotation(stageInfo.SpawnRotation[3]);
                players[scores[i]].GetComponentInChildren<Animator>().SetTrigger("Die");
            }
            else
            {
                players[scores[i]].GetComponent<PlayerIdentity>().SetToPosition(stageInfo.SpawnPosition[i]);
                players[scores[i]].GetComponent<PlayerIdentity>().SetToRotation(stageInfo.SpawnRotation[i]);
                players[scores[i]].GetComponentInChildren<Animator>().SetTrigger("Clap");
            }
        }
        yield return null;
    }

    // Set End scene

    // Load normal Scene
    public void LoadTestScene()
    {
        if (inputManager == null) return; // Not sure why I need this
        StartCoroutine(_LoadTestScene());
    }

    IEnumerator _LoadTestScene()
    {
        // TransitionsPanel.DOAnchorPosY(0, 0.4f);
        // TitleAudioSource.PlayOneShot(TitleAudioSource.clip);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().SetRagData();
        }
        yield return null;
    }

    // Load normal Scene
    public void LoadScene()
    {
        inputManager.enabled = false;
        StartCoroutine(_LoadScene());
    }

    IEnumerator _LoadScene()
    {
        // TransitionsPanel.DOAnchorPosY(0, 0.4f);
        // TitleAudioSource.PlayOneShot(TitleAudioSource.clip);
        yield return new WaitForSeconds(0.5f);

        foreach(GameObject g in PlayerCraftUIList)
        {
            Destroy(g);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().SetRagData();
        }
        yield return new WaitForSeconds(1.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CrownScene", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("CharacterChoose");
        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        OnBattleScene();
        yield return null;
    }
}
