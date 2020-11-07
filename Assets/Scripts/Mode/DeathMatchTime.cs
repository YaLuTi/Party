﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathMatchTime : DeathmatchBrawl
{
    public float Timer;
    bool IsEnd = false;
    public TextMeshProUGUI text;

    private void Update()
    {
        if(Timer <= 0)
        {
            IsEnd = true;
            Finish.Play();
        }
        else
        {
            Timer -= Time.deltaTime;
            Timer = Mathf.Max(0, Timer);
            text.text = ((int)(Timer/60)).ToString("00") + " : " + ((int)(Timer % 60)).ToString("00");
        }
    }

    public override void OnPlayerDeath(PlayerHitten playerHitten)
    {
        if (IsEnd) return;
        int num = playerHitten.LastDamagedID;
        if(num == -1)
        {
            num = playerHittens.IndexOf(playerHitten);
            Lifes[num]--;
        }
        else
        {
            Lifes[num]++;
        }
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
    }
}
