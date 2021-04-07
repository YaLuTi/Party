using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _LoadScene(string s)
    {
        StartCoroutine(Load(s));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator Load(string scene)
    {
        AsyncOperation asyncLoad;
        asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
