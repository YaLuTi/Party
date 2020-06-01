using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public Animation CameraAnimtion;
    int AnimationCount = 0;
    List<AnimationState> animationStates = new List<AnimationState>();

    public static bool IsPaused = false;
    public static GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag("Pause"))
                {
                    PausePanel = objs[i].gameObject;
                    break;
                }
            }
        }
        
        Time.timeScale = 1;
        IsPaused = false;
        // PausePanel.SetActive(false);
        AudioListener.pause = false;

        /*foreach (AnimationState state in CameraAnimtion)
        {
            animationStates.Add(state);
        }
        CameraAnimtion.Play(animationStates[AnimationCount].name);
        AnimationCount++;*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!CameraAnimtion.isPlaying && AnimationCount < animationStates.Count)
        {
            CameraAnimtion.Play(animationStates[AnimationCount].name);
            AnimationCount++;
        }*/
    }
    public void GameEnd()
    {
        Debug.Log("A");
        StageManager.StageStop();
    }
    public static void Pause()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
            PausePanel.SetActive(false);
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 0;
            IsPaused = true;
            PausePanel.SetActive(true);
            AudioListener.pause = true;
        }
    }
    public void UI_Pause()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;
            PausePanel.SetActive(false);
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 0;
            IsPaused = true;
            PausePanel.SetActive(true);
            AudioListener.pause = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
