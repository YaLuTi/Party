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
    public static StageManager instance;
    public static List<GameObject> players = new List<GameObject>();
    public static int[] PlayerProfile;
    public static int[] playerScore;
    public static bool InGame = false;
    public static PlayerInputManager inputManager;

    public static int[] scores; // 暫時弄成玩家名次

    static int PlayerReadyNum = 0;

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

    public GameObject Canvas;

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

        targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
        virtualCamera = GameObject.FindGameObjectWithTag("Cine").GetComponent<CinemachineVirtualCamera>();
        // if (Testing)GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
        // SceneManager.LoadScene("CharacterChoose", LoadSceneMode.Additive);
    }

    private void Start()
    {
        if (CreatingTest)
        {
            StageInfo stageInfo;
            stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
            for (int i = 0; i < 4; i++)
            {
                GameObject g = Instantiate(Player, stageInfo.SpawnPosition[i], Quaternion.Euler(stageInfo.SpawnRotation[i]));
                players.Add(g);
            }
        }
    }

    private void Update()
    {
        if (TriggerLoadScene)
        {
            TriggerLoadScene = false;
            EndDirector.Play();
            FacilityManager.UsingDirector = EndDirector;
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

            foreach(GameObject g in players)
            {
                g.GetComponentInChildren<PlayerCreating>().ChangeInput("GamePlay");
            }
            foreach(GameObject g in PlayerCraftUIList)
            {
                Destroy(g);
            }
            inputManager.enabled = false;
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
            StageInfo stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
            Vector3 v = stageInfo.SpawnPosition[players.Count - 1];
            v += new Vector3(2.25f, 2.5f, 0);
            g.GetComponent<Transform>().position = v;

            players[players.Count - 1].GetComponentInChildren<PlayerCreating>().profileChooseUI = g.GetComponent<ProfileChooseUI>();

            OnBattleScene += players[players.Count - 1].GetComponent<PlayerIdentity>().OnTest;

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
