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
    static int[] scores = new int[4]; // 暫時弄成玩家名次
    

    static TextMeshProUGUI[] texts;
    static bool IsEnd = false;
    static PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        IsEnd = false;
        playableDirector = GetComponent<PlayableDirector>();
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

            playableDirector.Play();
        }
    }
}
