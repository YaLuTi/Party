using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static List<GameObject> players = new List<GameObject>();
    PlayerInputManager inputManager;

    static int PlayerReadyNum = 0;

    static bool TriggerLoadScene = false;

    public GameObject PlayerCraftUI;
    public GameObject Canvas;
    public RectTransform TransitionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this.gameObject);
        }
        else if (this == instance)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Instantiate(players[i]);
            }
        }
    }

    private void Update()
    {
        if (TriggerLoadScene)
        {
            TriggerLoadScene = false;
            LoadScene();
        }
    }

    public static void StageStop()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputCancel();
        }
    }

    public void StageStart()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().InputEnable();
        }
    }

    public static void PlayerReady()
    {
        Debug.Log(1);
        PlayerReadyNum++;
    }

    public static void LoadSceneCheck()
    {
        Debug.Log(0);
        if(PlayerReadyNum >= players.Count)
        {
            TriggerLoadScene = true;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.transform.root.gameObject);
        players.Add((playerInput.transform.root.gameObject));
        GameObject g = Instantiate(PlayerCraftUI);
        g.transform.parent = Canvas.transform;
        //-280 -95
        g.GetComponent<RectTransform>().localPosition = new Vector3(-280 + ((players.Count - 1) * 185), 0, 0);
        players[players.Count - 1].GetComponentInChildren<PlayerCreating>().playerCreatingUI = g.GetComponent<PlayerCraftingUI>();
    }

    public void LoadScene()
    {
        inputManager.enabled = false;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerIdentity>().SetRagData();
        }
        StartCoroutine(_LoadScene());
    }

    IEnumerator _LoadScene()
    {
        TransitionsPanel.DOAnchorPosY(0, 0.4f);
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return null;
    }
}
