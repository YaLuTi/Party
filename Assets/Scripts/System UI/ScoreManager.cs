using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int _WinScore;
    public static int WinScore;
    static int[] scores = new int[4];
    static int[] rank;
    

    static TextMeshProUGUI[] texts;
    static bool IsEnd = false;
    static bool EndEvent = false;
    public PlayableDirector playableDirector;

    public GameObject Crown;
    // Start is called before the first frame update
    void Start()
    {
        IsEnd = false;
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        for(int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
        WinScore = _WinScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (EndEvent)
        {
            EndEvent = false;
            playableDirector.Play();
            StartCoroutine(_FinishEvent());
        }
    }

    public static void AddScore(int id, int score)
    {
        if (IsEnd) return;
        scores[id] += score;
        for(int i =0; i < texts.Length; i++)
        {
            texts[i].text = scores[i].ToString();
        }

        if(scores[id] >= WinScore)
        {
            IsEnd = true;
            EndEvent = true;
        }
    }

    IEnumerator _FinishEvent()
    {
        IsEnd = true;
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        StageManager.StopBGM();
        BattleData.TEST_END_SHOW();
        
        rank = new int[scores.Length];
        for (int i = 0; i < rank.Length; i++)
        {
            rank[i] = i;
        }

        Array.Sort(scores, rank);
        Array.Reverse(rank);

        for(int i = 0; i < rank.Length; i++)
        {
            Debug.Log(rank[i]);
        }

        yield return new WaitForSecondsRealtime(1.35f);
        StageManager.SetCloseUpCamera(rank[0]);
        yield return new WaitForSecondsRealtime(2f);
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().LoadLobby();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;

        yield return null;
    }

    private void OnDestroy()
    {
        Destroy(Crown);
    }
}
