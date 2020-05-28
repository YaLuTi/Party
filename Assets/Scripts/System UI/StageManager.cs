using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public List<GameObject> players = new List<GameObject>();

    public RectTransform TransitionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }else if(this != instance)
        {
            Destroy(this.gameObject);
        }else if (this == instance)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Instantiate(players[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.transform.root.gameObject);
        players.Add((playerInput.transform.root.gameObject));
    }

    public void LoadScene()
    {
        for(int i = 0; i < players.Count; i++)
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
        yield return new WaitForSeconds(1.5f);
        TransitionsPanel.DOAnchorPosY(510, 0.4f);
        yield return null;
    }
}
