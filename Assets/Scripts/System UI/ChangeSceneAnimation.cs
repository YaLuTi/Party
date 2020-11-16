using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;

public class ChangeSceneAnimation : MonoBehaviour
{
    public static ChangeSceneAnimation instance;

    public PlayableDirector Mask;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackIn()
    {
        Mask.Play();
    }
    public void BlackOut()
    {
        Mask.Play();
    }
}
