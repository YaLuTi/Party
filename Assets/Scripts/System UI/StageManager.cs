using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Playables;
using Cinemachine;
using AnimFollow;

public delegate void OnBattleScene();

public class StageManager : MonoBehaviour
{
    public OnBattleScene OnBattleScene;
    public static StageManager instance;
    public static List<GameObject> players = new List<GameObject>();
    public static int[] PlayerProfile;
    public static int[] playerScore;
    public static bool InGame = false;
    public static bool InLobby = false;
    public static bool LoadWin = false;
    public static PlayerInputManager inputManager;

    public static int[] scores; // 暫時弄成玩家名次

    static int PlayerReadyNum = 0;

    static int[] WinPlayer;

    static bool TriggerLoadScene = false;
    static bool TriggerLoadEnd = false;

    public bool Testing = false;
    public static bool Static_Testing = true;

    public static CinemachineTargetGroup targetGroup;
    public static CinemachineVirtualCamera virtualCamera;
    public PlayableDirector EndDirector;

    // 要移除
    public GameObject PlayerCraftUI;
    public static List<GameObject> PlayerCraftUIList = new List<GameObject>();

    public SceneChangeTest changeTest;

    public GameObject Canvas;
    public GameObject HintText;

    public delegate void PlayerJoinHandler(GameObject player, int num);
    public event PlayerJoinHandler OnPlayerJoin;

    public GameObject Player;
    public bool CreatingTest = true;



    // Start is called before the first frame update
    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        PlayerProfile = new int[4];

        if (instance == null)
        {
            Static_Testing = Testing;
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

        if (GameObject.FindGameObjectWithTag("CineGroup") != null)
        {
            targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
            virtualCamera = GameObject.FindGameObjectWithTag("Cine").GetComponent<CinemachineVirtualCamera>();
        }
        // if (Testing)GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
        // SceneManager.LoadScene("CharacterChoose", LoadSceneMode.Additive);
    }

    private void Start()
    {
        Invoke("TestCreate", 0);
    }

    void TestCreate()
    {
        if (CreatingTest)
        {
            StageInfoTransform stageInfo;
            stageInfo = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfoTransform>();
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("RTH");
                GameObject g = Instantiate(Player, stageInfo.transforms[i].position, Quaternion.Euler(stageInfo.transforms[i].eulerAngles));
                players.Add(g);
            }
        }
    }

    private void Update()
    {
        if (TriggerLoadScene)
        {
            TriggerLoadScene = false;
            if(GameObject.FindGameObjectWithTag("EndDirector") != null)
            {
                GameObject.FindGameObjectWithTag("EndDirector").GetComponent<PlayableDirector>().Play();
                inputManager.enabled = false;
            }
            // EndDirector.Play();
            // FacilityManager.UsingDirector = EndDirector;
        }

        if (LoadWin)
        {
            LoadWin = false;
            StartCoroutine(WinDelay());
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

    public static void UIOn()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputUI();
        }
    }

    public void StageStart()
    {
        InGame = true;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().PlayerInputEnable();
        }
        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<PlayerInput>().SwitchCurrentActionMap("GamePlay");
        }
    }
       
    // Title UI
    public static void PlayerReady()
    {
        PlayerReadyNum++;
    }
    public static void PlayerUnReady()
    {
        PlayerReadyNum--;
    }

    public static void DisablePlayerControl()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputCancel();
        }
    }

    public static void EnablePlayerControl()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputEnable();
        }
    }

    // 這邊一團亂 要整理！！！！！！！！！！！
    public static void LoadSceneCheck()
    {
        if(PlayerReadyNum >= players.Count)
        {
            playerScore = new int[players.Count];
            scores = new int[players.Count];
            TriggerLoadScene = true;

            foreach (GameObject g in PlayerCraftUIList)
            {
                Destroy(g);
            }
            // inputManager.enabled = false;
            // inputManager.enabled = false;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (instance != this) return;
        if (Testing)
        {
            Debug.Log("Add");
            playerInput.transform.root.GetComponent<PlayerIdentity>().PlayerInputEnable();
            // playerInput.SwitchCurrentActionMap("GamePlay");
            // Destroy(playerInput.transform.root.GetComponentInChildren<PlayerCreating>());
            players.Add((playerInput.transform.root.gameObject));

            if(targetGroup != null)targetGroup.AddMember(playerInput.transform, 1, 0);

            if(OnPlayerJoin != null)
            {
                OnPlayerJoin(playerInput.gameObject, players.Count - 1);
            }
            LoadTestScene();
            // playerInput.transform.root.GetComponent<PlayerIdentity>().SetRagData();
        }
        else
        {
            if (CreatingTest) return;
            Debug.Log("Add");
            DontDestroyOnLoad(playerInput.transform.root.gameObject);
            players.Add((playerInput.transform.root.gameObject));

            GameObject g = Instantiate(PlayerCraftUI);
            StageInfoTransform stageInfo = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfoTransform>();
            Vector3 v = stageInfo.transforms[players.Count - 1].position;
            v += new Vector3(2.25f, 2.5f, 0);
            g.GetComponent<Transform>().position = v;

            players[players.Count - 1].GetComponentInChildren<PlayerCreating>().profileChooseUI = g.GetComponent<ProfileChooseUI>();
            playerInput.SwitchCurrentActionMap("Creating(UI)");

            OnBattleScene += players[players.Count - 1].GetComponent<PlayerIdentity>().OnTest;
            players[players.Count - 1].GetComponent<PlayerIdentity>().SetToTransform();

            if (targetGroup != null) targetGroup.AddMember(playerInput.transform, 1, 0);
            PlayerCraftUIList.Add(g);
            if (OnPlayerJoin != null)
            {
                OnPlayerJoin(playerInput.gameObject, players.Count - 1);
            }
            // LoadTestScene();
        }
    }
    
    public static void PlayBGM()
    {
        Debug.Log("a");
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
    }

    public static void StopBGM()
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Stop();
    }

    // Set End scene

    // Load normal Scene
    public void LoadTestScene()
    {
        if (inputManager == null) return; // Not sure why I need this
        StartCoroutine(_LoadTestScene());
    }

    public void LoadLobby()
    {
        foreach (GameObject player in players)
        {
            player.GetComponentInChildren<SimpleFootIK_AF>().followTerrain = false;
        }
        changeTest.LoadLobby("BlockoutTest 2");
        if (!SceneChangeTest.IsLoadingTutorial)
        {
            InLobby = true;
        }
    }
    public static void ThrowPlayer(int i)
    {
        /*player.GetComponentInChildren<SimpleFootIK_AF>().followTerrain = true;
        player.GetComponentInChildren<PlayerMove>().AddForceSpeed(new Vector3(0, 0, 200));*/
        if (i >= players.Count) return;

        BulletHitInfo_AF bulletHitInfo_AF = new BulletHitInfo_AF();

        Collider[] colliders = players[i].GetComponent<PlayerHitten>().Hips.GetComponentsInChildren<Collider>();
        // Vector3 p = player.GetComponent<PlayerHitten>().Hips.position - new Vector3( 0, 0, -1);
        Vector3 p = new Vector3(0, 3f, -15.5f + i * -1f);

        int j = 0;
        foreach (Collider collider in colliders)
        {
            if (j % 2 != 0) continue;
            bulletHitInfo_AF.IsShot = true;
            bulletHitInfo_AF.hitTransform = collider.transform;
            bulletHitInfo_AF.bulletForce = (collider.ClosestPoint(p) - p).normalized * (20000 + i * 10000);
            bulletHitInfo_AF.hitPoint = collider.ClosestPoint(p);

            players[i].GetComponent<PlayerHitten>().EventOnHit(bulletHitInfo_AF);
            players[i].GetComponent<PlayerIdentity>().InputEnable();
            j++;
        }
        players[i].GetComponentInChildren<SimpleFootIK_AF>().followTerrain = true;
        /*foreach (GameObject player in players)
        {
        }*/
    }

    IEnumerator _LoadTestScene()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().SetRagData();
        }
        yield return null;
    }

    public static void ClearPlayer()
    {
        InGame = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;
        foreach (GameObject g in players)
        {
            g.GetComponent<PlayerIdentity>().Freeze();
            g.GetComponentInChildren<PlayerBehavior>().OnHit();

        }
        if (!StageManager.Static_Testing) return;
        players.Clear();
    }

    public static void RemoveCameraTarget(Transform transform)
    {
        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        targetGroup.RemoveMember(transform);
    }

    public static void RemoveCameraTarget(int i)
    {
        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets[i+1].target = null;
    }

    public static void SetCloseUpCamera(int i)
    {
        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets = null;
        targetGroup.AddMember(players[i].GetComponent<PlayerHitten>().Hips, 1, 0);
        virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 5;
        // virtualCamera.transform.eulerAngles = players[i].GetComponentInChildren<PlayerInput>().transform.eulerAngles * -1;
    }

    public void GameEnd(int[] Winplayer)
    {
        WinPlayer = Winplayer;
        InGame = false;
        changeTest = GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>();
        changeTest.LoadWinScene();
    }

    public static void SetWin()
    {
        for(int i = 0; i < WinPlayer.Length; i++)
        {
        }
        players[WinPlayer[0]].GetComponent<PlayerIdentity>().SetToAnimationMode();
        players[WinPlayer[0]].GetComponent<PlayerIdentity>().SetToTransform();

        players[WinPlayer[0]].GetComponent<PlayerHitten>().pickItem.OnHit();
        GameObject.FindGameObjectWithTag("WinSceneTimeline").GetComponent<PlayableDirector>().Play();
        // players[WinPlayer[0]].GetComponentInChildren<Animator>().applyRootMotion = true;
        players[WinPlayer[0]].GetComponentInChildren<Animator>().SetTrigger("Win");
        LoadWin = true;
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(5);
        InLobby = true;
        changeTest.LoadLobby("BlockoutTest 2");
        yield return null;
    }

    public static void LoadNewScene()
    {
        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        virtualCamera = GameObject.FindGameObjectWithTag("Cine").GetComponent<CinemachineVirtualCamera>();
        if (targetGroup != null)
        {
            for (int i = 0; i < players.Count; i++)
            {
                targetGroup.AddMember(players[i].GetComponentInChildren<PlayerInput>().transform, 1, 0);
            }
        }

        foreach(GameObject g in players)
        {
            g.GetComponent<PlayerHitten>().Refresh();
        }
    }
}
