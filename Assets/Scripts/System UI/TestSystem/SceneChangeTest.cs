using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTest : MonoBehaviour
{
    public static SceneChangeTest instance;

    public string[] SceneArray;
    int SceneChoosing = 0;

    public string[] ModeArray;
    int ModeChoosing = 0;

    int NowScene = 0;
    int NowMode = 0;

    float dpiScale;
    public GUIStyle guiStyleHeader = new GUIStyle();

    public ChangeSceneAnimation sceneAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            SceneManager.LoadSceneAsync(ModeArray[ModeChoosing], LoadSceneMode.Additive);
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
        
    }

    bool isButtonPressed;
    private void OnGUI()
    {
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

        sceneAnimation.BlackIn();
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.UnloadSceneAsync(ModeArray[NowMode]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        PlayerHitten.TestHP = 0;
        sceneAnimation.BlackOut();

        /*asyncLoad = SceneManager.LoadSceneAsync(SceneArray[scene]);
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

        sceneAnimation.BlackOut();
        yield return new WaitForSeconds(0.5f);
        StageManager.LoadNewScene();*/
        /*asyncLoad = SceneManager.LoadSceneAsync(ModeArray[ModeChoosing], LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NowMode = ModeChoosing;*/

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
}
