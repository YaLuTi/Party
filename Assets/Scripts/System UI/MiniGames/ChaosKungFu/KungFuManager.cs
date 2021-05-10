using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KungFuManager : MonoBehaviour
{
    static SceneChangeTest changeTest;
    public GameObject KungFuPlayer;
    public static List<GameObject> players = new List<GameObject>();
    static int PlayerLifes;

    public static bool IsFirst = true;
    public PlayableDirector StartTimeline;
    public PlayableDirector TutorialTimeline;
    // Start is called before the first frame update
    void Start()
    {
        players.Clear();
        StageManager.instance.OnPlayerJoin += PlayerJoin;
        for(int i = 0; i < StageManager.players.Count; i++)
        {
            Vector3 p = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnPosition[i];
            Vector3 r = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnRotation[i];
            GameObject g = Instantiate(KungFuPlayer, p, Quaternion.Euler(r));
            g.GetComponent<KungFuPlayerControll>().Set(StageManager.players[i], i);
            g.GetComponent<KungFuPlayerControll>().ClothNum = StageManager.players[i].GetComponent<PlayerIdentity>().ClothNum;
            g.GetComponent<KungFuPlayerControll>().Playernum = i;
            g.GetComponentInChildren<PlayerCreating>().CreatMini();
            players.Add(g);
            PlayerLifes = StageManager.players.Count;
        }
        changeTest = GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>();

    }

    public void StartGame()
    {
        if (!IsFirst)
        {
            StartTimeline.Play();
        }
        else
        {
            TutorialTimeline.Play();
        }
        IsFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayerJoin(GameObject player, int num)
    {
        Vector3 p = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnPosition[num];
        Vector3 r = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnRotation[num];
        GameObject g = Instantiate(KungFuPlayer, p, Quaternion.Euler(r));
        g.GetComponent<KungFuPlayerControll>().Set(player, num);
        PlayerLifes++;
        Debug.Log(PlayerLifes);
        g.GetComponent<KungFuPlayerControll>().ClothNum[0] = num;
        g.GetComponentInChildren<PlayerCreating>().CreatMini();
        players.Add(g);
    }

    public static void PlayerDeath()
    {
        PlayerLifes--;
        Debug.Log(PlayerLifes);
        GameObject[] gs = GameObject.FindGameObjectsWithTag("DeskObject");
        foreach(GameObject g in gs)
        {
            if ((int)Random.Range(0, 2) == 0)
            {
                g.GetComponent<AudienceAnimator>().Play("Cheer");
            }
            else
            {
                g.GetComponent<AudienceAnimator>().Play("Clap");
            }
        }
        if (PlayerLifes <= 1)
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<PlayableDirector>().Play();
        }
    }

    public void LoadLooby()
    {
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().LoadLobby();
    }

    public void LoadMini()
    {
        GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>().LoadMiniGame("MiniGame");
    }
}
