using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Title : MonoBehaviour
{
    [SerializeField]
    string[] Scenes;
    SceneChangeTest changeTest;

    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(Scenes[0], LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        playableDirector.Play();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayableDirector>().Play();
        yield return null;
    }
}
