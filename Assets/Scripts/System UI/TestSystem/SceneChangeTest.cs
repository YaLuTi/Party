using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneChangeTest : MonoBehaviour
{
    public static SceneChangeTest instance;

    public static bool IsLoadingTutorial = true;

    public string[] SceneArray;
    int SceneChoosing = 0;

    public string[] ModeArray;
    int ModeChoosing = 0;

    int NowScene = 0;
    int NowMode = 0;

    public bool Title = false;

    float dpiScale;
    public GUIStyle guiStyleHeader = new GUIStyle();

    public bool ISGUI;

    public ChangeSceneAnimation sceneAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            if (!Title)
            {
                SceneManager.LoadSceneAsync(ModeArray[ModeChoosing], LoadSceneMode.Additive);
            }
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }

        if (Screen.dpi < 1) dpiScale = 1;
        if (Screen.dpi < 200) dpiScale = 1;
        else dpiScale = Screen.dpi / 200f;
        guiStyleHeader.fontSize = (int)(15f * dpiScale);

        // SceneManager.LoadSceneAsync("EmptyScene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetButtonDown("Cancel"))
        {
            ISGUI = !ISGUI;
        }*/
    }

    bool isButtonPressed;
    private void OnGUI()
    {
        if (!ISGUI) return;
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            isButtonPressed = false;

        if (GUI.Button(new Rect(10 * dpiScale, 15 * dpiScale, 135 * dpiScale, 37 * dpiScale), "PREVIOUS SCENE") || (!isButtonPressed && Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            isButtonPressed = true;
            ChangeScene(-1);
        }
        if (GUI.Button(new Rect(160 * dpiScale, 15 * dpiScale, 135 * dpiScale, 37 * dpiScale), "NEXT SCENE") || (!isButtonPressed && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isButtonPressed = true;
            ChangeScene(1);
        }
        if (GUI.Button(new Rect(10 * dpiScale, 52 * dpiScale, 135 * dpiScale, 37 * dpiScale), "PREVIOUS MODE") || (!isButtonPressed && Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            isButtonPressed = true;
            ChangeMode(-1);
        }
        if (GUI.Button(new Rect(160 * dpiScale, 52 * dpiScale, 135 * dpiScale, 37 * dpiScale), "NEXT MODE") || (!isButtonPressed && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isButtonPressed = true;
            ChangeMode(1);
        }
        var offset = 0f;

        if (GUI.Button(new Rect(10 * dpiScale, 93 * dpiScale + offset, 285 * dpiScale, 37 * dpiScale), "LOAD"))
        {
            LoadScene(ModeChoosing, SceneChoosing);
        }

        /*GUI.Label(new Rect(350 * dpiScale, 15 * dpiScale + offset / 2, 500 * dpiScale, 20 * dpiScale),
            "press left mouse button for the camera rotating and scroll wheel for zooming", guiStyleHeader);*/

        GUI.Label(new Rect(350 * dpiScale, 15 * dpiScale + offset / 2, 160 * dpiScale, 20 * dpiScale),
            "Scene : " + SceneArray[SceneChoosing], guiStyleHeader);
        GUI.Label(new Rect(350 * dpiScale, 30 * dpiScale + offset / 2, 160 * dpiScale, 20 * dpiScale),
            "Mode : " + ModeArray[ModeChoosing], guiStyleHeader);
    }

    public void LoadScene(int mode, int scene)
    {
        StartCoroutine(_LoadScene(mode, scene));
    }

    IEnumerator _LoadScene(int mode, int scene)
    {
        StageManager.ClearPlayer();
        StageManager.InLobby = false;

        sceneAnimation.BlackIn();

        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad;
        if (!Title)
        {
            asyncLoad = SceneManager.UnloadSceneAsync(ModeArray[NowMode]);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        PlayerHitten.TestHP = 0;

        // sceneAnimation.BlackOut();

        asyncLoad = SceneManager.LoadSceneAsync(SceneArray[scene]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NowScene = scene;

        yield return new WaitForFixedUpdate();
        
            asyncLoad = SceneManager.LoadSceneAsync(ModeArray[mode], LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            NowMode = mode;

        yield return new WaitForSeconds(0.5f);

        sceneAnimation.BlackOut();

        StageManager.LoadNewScene();
        /*asyncLoad = SceneManager.LoadSceneAsync(ModeArray[ModeChoosing], LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NowMode = ModeChoosing;*/

        yield return null;
    }

    public void LoadLobby(string scene)
    {
        StartCoroutine(_LoadLobby(scene));
    }
    IEnumerator _LoadLobby(string scene)
    {
        StageManager.ClearPlayer();
        yield return new WaitForFixedUpdate();
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        StageManager.LoadNewScene();
        if (IsLoadingTutorial)
        {
            GameObject.FindGameObjectWithTag("Tutorial").GetComponent<PlayableDirector>().Play();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Enter").GetComponent<PlayableDirector>().Play();
        }
        yield return null;
    }

    public void LoadMiniGame(string scene)
    {
        StartCoroutine(_LoadMiniGame(scene));
    }
    IEnumerator _LoadMiniGame(string scene)
    {
        StageManager.ClearPlayer();
        yield return new WaitForFixedUpdate();
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        StageManager.LoadMiniGame();
        // StageManager.LoadNewScene();
        yield return null;
    }

    public void ThrowPlayer()
    {
        StartCoroutine(_ThrowPlayer());
    }

    IEnumerator _ThrowPlayer()
    {
        StageManager.ThrowPlayer(0);
        yield return new WaitForSeconds(0.3f);
        StageManager.ThrowPlayer(1);
        yield return new WaitForSeconds(0.3f);
        StageManager.ThrowPlayer(2);
        yield return new WaitForSeconds(0.3f);
        StageManager.ThrowPlayer(3);

        if (IsLoadingTutorial)
        {
            IsLoadingTutorial = false;
        }
        yield return null;
    }

    void ChangeScene(int delta)
    {
        SceneChoosing += delta;
        if (SceneChoosing > SceneArray.Length - 1)
            SceneChoosing = 0;
        else if (SceneChoosing < 0)
            SceneChoosing = SceneArray.Length - 1;
    }
    void ChangeMode(int delta)
    {
        ModeChoosing += delta;
        if (ModeChoosing > ModeArray.Length - 1)
            ModeChoosing = 0;
        else if (ModeChoosing < 0)
            ModeChoosing = ModeArray.Length - 1;
    }

    public void LoadWinScene()
    {
        StartCoroutine(_LoadWinScene());
    }

    IEnumerator _LoadWinScene()
    {
        yield return new WaitForSeconds(0.5f);
        StageManager.DisablePlayerControl();
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(SceneArray[NowScene]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        StageManager.SetWin();
        yield return null;
    }
}
