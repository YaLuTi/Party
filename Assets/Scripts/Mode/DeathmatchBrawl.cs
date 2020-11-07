using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DeathmatchBrawl : MonoBehaviour
{
    public PlayableDirector Finish;
    public GameObject UI;
    public List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    public List<GameObject> UIs = new List<GameObject>();
    public int DefaultLife = 3;
    public List<int> Lifes = new List<int>();

    static bool First = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!First)
        {
            StageManager.instance.OnPlayerJoin += PlayerJoin;
            First = true;
        }
        for(int i = 0; i < StageManager.players.Count; i++)
        {
            StageManager.players[i].GetComponent<PlayerHitten>().OnDeath += OnPlayerDeath;
            playerHittens.Add(StageManager.players[i].GetComponent<PlayerHitten>());
            Lifes.Add(DefaultLife);
            GameObject g = Instantiate(UI);
            g.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            switch (i)
            {
                case 0:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 190);
                    g.GetComponent<Text>().color = Color.blue;
                    break;
                case 1:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, 190);
                    g.GetComponent<Text>().color = Color.red;
                    break;
                case 2:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, -190);
                    g.GetComponent<Text>().color = Color.yellow;
                    break;
                case 3:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, -190);
                    g.GetComponent<Text>().color = Color.green;
                    break;
            }
            UIs.Add(g);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerJoin(GameObject player, int num)
    {
        Debug.Log(num);
        StageManager.players[num].GetComponent<PlayerHitten>().OnDeath += OnPlayerDeath;
        playerHittens.Add(StageManager.players[num].GetComponent<PlayerHitten>());
        Lifes.Add(DefaultLife);
        GameObject g = Instantiate(UI);
        g.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        switch (num)
        {
            case 0:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 190);
                g.GetComponent<Text>().color = Color.blue;
                break;
            case 1:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, 190);
                g.GetComponent<Text>().color = Color.red;
                break;
            case 2:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, -190);
                g.GetComponent<Text>().color = Color.yellow;
                break;
            case 3:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, -190);
                g.GetComponent<Text>().color = Color.green;
                break;
        }
        UIs.Add(g);
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
    }

    public virtual void OnPlayerDeath(PlayerHitten playerHitten)
    {
        int num = playerHittens.IndexOf(playerHitten);
        Lifes[num]--;
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
        if(Lifes[num] <= 0)
        {
            UIs[num].GetComponent<Text>().color = Color.red;
            playerHitten.SetRespawnable(false);
            StageManager.RemoveCameraTarget(playerHitten.playerMove.transform);
        }
    }

}
