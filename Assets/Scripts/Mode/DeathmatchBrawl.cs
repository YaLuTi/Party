using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathmatchBrawl : MonoBehaviour
{
    public GameObject UI;
    public List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    public List<GameObject> UIs = new List<GameObject>();
    public List<int> Lifes = new List<int>(); 

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().OnPlayerJoin += PlayerJoin;
        for(int i = 0; i < StageManager.players.Count; i++)
        {
            StageManager.players[i].GetComponent<PlayerHitten>().OnDeath += OnPlayerDeath;
            playerHittens.Add(StageManager.players[i].GetComponent<PlayerHitten>());
            Lifes[i] = 3;
            GameObject g = Instantiate(UI);
            g.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            switch (i)
            {
                case 0:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 190);
                    g.GetComponent<Text>().color = Color.green;
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
        StageManager.players[num].GetComponent<PlayerHitten>().OnDeath += OnPlayerDeath;
        playerHittens.Add(StageManager.players[num].GetComponent<PlayerHitten>());
        Lifes.Add(3);
        GameObject g = Instantiate(UI);
        g.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        switch (num)
        {
            case 0:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 190);
                g.GetComponent<Text>().color = Color.green;
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
                break;
        }
        UIs.Add(g);
    }
    
    void OnPlayerDeath(PlayerHitten playerHitten)
    {
        int num = playerHittens.IndexOf(playerHitten);
        Lifes[num]--;
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
        if(Lifes[num] <= 0)
        {
            UIs[num].GetComponent<Text>().color = Color.red;
            playerHitten.SetRespawnable(false);
        }
    }

}
