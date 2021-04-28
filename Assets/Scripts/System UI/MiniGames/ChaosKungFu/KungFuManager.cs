﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class KungFuManager : MonoBehaviour
{
    static SceneChangeTest changeTest;
    public GameObject KungFuPlayer;
    static int PlayerLifes;
    // Start is called before the first frame update
    void Start()
    {
        StageManager.instance.OnPlayerJoin += PlayerJoin;
        for(int i = 0; i < StageManager.players.Count; i++)
        {
            Vector3 p = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnPosition[i];
            Vector3 r = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnRotation[i];
            GameObject g = Instantiate(KungFuPlayer, p, Quaternion.Euler(r));
            g.GetComponent<KungFuPlayerControll>().Set(StageManager.players[i], i);
            g.GetComponent<KungFuPlayerControll>().HelmetNum = StageManager.players[i].GetComponent<PlayerIdentity>().Helmetnum;
            g.GetComponentInChildren<PlayerCreating>().CreatMini();
            PlayerLifes = StageManager.players.Count;
        }
        changeTest = GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>();
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
        g.GetComponent<KungFuPlayerControll>().HelmetNum = num;
        g.GetComponentInChildren<PlayerCreating>().CreatMini();
    }

    public static void PlayerDeath()
    {
        PlayerLifes--;
        Debug.Log(PlayerLifes);
        if (PlayerLifes <= 1)
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<PlayableDirector>().Play();
        }
    }

    public void LoadLooby()
    {
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().LoadLobby();
    }
}
