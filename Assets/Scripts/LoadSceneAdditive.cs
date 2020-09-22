using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAdditive : MonoBehaviour
{
    public bool LoadBattle;
    public bool LoadTitle;
    public bool Test;
    // Start is called before the first frame update
    void Start()
    {
        if (LoadTitle)
        {
            SceneManager.LoadScene("CharacterChoose", LoadSceneMode.Additive);
        }
        if (LoadBattle)
        {
            SceneManager.LoadScene("CrownScene", LoadSceneMode.Additive);
        }
        if (Test)
        {
            SceneManager.LoadScene("SceneForTest 1", LoadSceneMode.Additive);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
