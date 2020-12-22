using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DeathmatchBrawl : MonoBehaviour
{
    public PlayableDirector Finish;
    public GameObject UI;
    public static List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    public static List<GameObject> UIs = new List<GameObject>();
    public int DefaultLife = 3;
    public static List<int> Lifes = new List<int>();
    public static List<int> DeadPlayer = new List<int>();

    static bool First = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        UIs.Clear();
        Lifes.Clear();
        playerHittens.Clear();
        DeadPlayer.Clear();

        StageManager.instance.OnPlayerJoin += PlayerJoin;

        if (!First)
        {
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
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 170);
                    g.GetComponent<Text>().color = new Color(0.5f,1,1);
                    break;
                case 1:
                    g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, 170);
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
            UIs[i].GetComponent<Text>().text = Lifes[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void PlayerJoin(GameObject player, int num)
    {
        Debug.Log(num);
        StageManager.players[num].GetComponent<PlayerHitten>().OnDeath += OnPlayerDeath;
        playerHittens.Add(StageManager.players[num].GetComponent<PlayerHitten>());

        Lifes.Add(DefaultLife);
        Debug.Log(Lifes.Count);

        GameObject g = Instantiate(UI);
        g.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        switch (num)
        {
            case 0:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(-330, 170);
                g.GetComponent<Text>().color = new Color(0.5f, 1, 1);
                break;
            case 1:
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(330, 170);
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
        Debug.Log(UIs.Count);
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
    }

    public virtual void OnPlayerDeath(PlayerHitten playerHitten)
    {
        int num = playerHittens.IndexOf(playerHitten);
        Lifes[num]--;
        UIs[num].GetComponent<Text>().text = Lifes[num].ToString();
        Debug.Log(Lifes[num].ToString());
        if(Lifes[num] <= 0)
        {
            UIs[num].GetComponent<Text>().color = Color.red;
            playerHitten.SetRespawnable(false);
            StageManager.RemoveCameraTarget(playerHitten.playerMove.transform);
            DeadPlayer.Add(num);
            if (DeadPlayer.Count >= Lifes.Count - 1)
            {
                Finish.Play();
                StartCoroutine(_FinishEvent());
            }
        }
    }
    IEnumerator _FinishEvent()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        StageManager.StopBGM();
        BattleData.TEST_END_SHOW();

        yield return new WaitForSecondsRealtime(1.35f);
        for(int i = 0; i < Lifes.Count; i++)
        {
            if (DeadPlayer.Contains(i))
            {

            }
            else
            {
                Debug.Log(i);
                StageManager.SetCloseUpCamera(i);
            }
        }
        // StageManager.SetCloseUpCamera(rank[0]);
        yield return new WaitForSecondsRealtime(2f);
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().LoadLobby();
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.01f;

        yield return null;
    }

    public virtual void FinishEvent()
    {
    }

    private void OnDestroy()
    {
        Debug.Log("DDDDDDDD");
        for (int i = 0; i < StageManager.players.Count; i++)
        {
            StageManager.players[i].GetComponent<PlayerHitten>().OnDeath -= OnPlayerDeath;
        }
            StageManager.instance.OnPlayerJoin -= PlayerJoin;
    }
}
