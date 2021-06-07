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
    

    static bool IsEnd = false;
    static bool EndEvent = false;
    public PlayableDirector playableDirector;

    public static TextMeshProUGUI[] texts;

    public GameObject UI;
    public GameObject Crown;
    // Start is called before the first frame update
    void Start()
    {
        IsEnd = false;
        texts = new TextMeshProUGUI[4];
        for (int i = 0; i < StageManager.players.Count; i++)
        {
            GameObject g = Instantiate(UI);
            g.transform.parent = transform;
            g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            texts[i] = g.GetComponentInChildren<TextMeshProUGUI>();
            g.GetComponentInChildren<RenderTextureUI>().num = i;
        }
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
        texts[id].text = scores[id].ToString();
        

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
