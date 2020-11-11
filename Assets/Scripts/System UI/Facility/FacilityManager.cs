using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FacilityManager : MonoBehaviour
{
    public static bool IsMenu = false;
    public static bool IsPlay = false;
    public static PlayableDirector UsingDirector;
    // Start is called before the first frame update
    void Start()
    {
        IsMenu = false;
        IsPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
