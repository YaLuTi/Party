using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeathMatchTime : DeathmatchBrawl
{
    public float Timer;
    public static bool IsEnd = false;
    public bool IsStart = false;
    public TextMeshProUGUI text;

    public override void Start()
    {
        Debug.Log("WGERDG");
        IsEnd = false;
        IsStart = false;
        base.Start();
    }

    private void Update()
    {
        text.text = ((int)(Timer / 60)).ToString("00") + " : " + ((int)(Timer % 60)).ToString("00");
        if (!IsStart) return;
        if (Timer <= 0)
        {
            if (!IsEnd)
            {
                IsEnd = true;

                // Finish改成由SceneManager發

                Finish.Play();
                FinishEvent();
                StartCoroutine(_FinishEvent());
            }
        }
        else
        {
            Timer -= Time.deltaTime;
            Timer = Mathf.Max(0, Timer);
            // text.text = ((int)(Timer/60)).ToString("00") + " : " + ((int)(Timer % 60)).ToString("00");
        }
    }

    public void GameStart()
    {
        IsStart = true;
    }

    public override void PlayerJoin(GameObject player, int num)
    {
        base.PlayerJoin(player, num);
    }
    public override void FinishEvent()
    {
        base.FinishEvent();
    }
    IEnumerator _FinishEvent()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        StageManager.StopBGM();
        BattleData.TEST_END_SHOW();

        int[] rank;
        int[] scores;
        rank = new int[Lifes.Count];
        scores = new int[Lifes.Count];
        for (int i = 0; i < rank.Length; i++)
        {
            rank[i] = i;
        }
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = Lifes[i];
        }

        Array.Sort(scores, rank);
        Array.Reverse(rank);

        yield return new WaitForSecondsRealtime(1.35f);
        StageManager.SetCloseUpCamera(rank[0]);

        yield return null;
    }
    public override void OnPlayerDeath(PlayerHitten playerHitten)
    {
        if (IsEnd)
        {
            Debug.Log(IsEnd);
            return;
        }
        int num = playerHitten.LastDamagedID;
        if(num == -1)
        {
            num = playerHittens.IndexOf(playerHitten);
            Lifes[num]--;
        }
        else
        {
            Debug.Log(num);
            Lifes[num]++;
        }
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
    }
}
