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
    int ModeChoosing = 1;

    int NowScene = 0;
    int NowMode = 0;

    float dpiScale;
    public GUIStyle guiStyleHeader = new GUIStyle();
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
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
        var offset = 0f;

        if (GUI.Button(new Rect(10 * dpiScale, 63 * dpiScale + offset, 285 * dpiScale, 37 * dpiScale), "LOAD") || (!isButtonPressed && Input.GetKeyDown(KeyCode.DownArrow)))
        {
            LoadScene(ModeChoosing, SceneChoosing);
        }

        /*GUI.Label(new Rect(350 * dpiScale, 15 * dpiScale + offset / 2, 500 * dpiScale, 20 * dpiScale),
            "press left mouse button for the camera rotating and scroll wheel for zooming", guiStyleHeader);*/
        GUI.Label(new Rect(350 * dpiScale, 15 * dpiScale + offset / 2, 160 * dpiScale, 20 * dpiScale),
            "Scene : " + SceneArray[SceneChoosing], guiStyleHeader);
    }

    public void LoadScene(int mode, int scene)
    {
        StartCoroutine(_LoadScene(mode, scene));
    }

    IEnumerator _LoadScene(int mode, int scene)
    {
        StageManager.ClearPlayer();
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.UnloadSceneAsync(ModeArray[NowMode]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        PlayerHitten.TestHP = 0;

        asyncLoad = SceneManager.LoadSceneAsync(SceneArray[scene]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NowScene = scene;

        yield return new WaitForFixedUpdate();

        if (!StageManager.Static_Testing)
        {
            asyncLoad = SceneManager.LoadSceneAsync(ModeArray[mode], LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            NowMode = mode;
        }
        StageManager.LoadNewScene();
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
}
