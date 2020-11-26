using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventController : MonoBehaviour
{
    public List<SceneEvent> sceneEvents = new List<SceneEvent>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(SceneEvent sceneEvent in sceneEvents)
        {
            sceneEvent.Object.InvokeRepeating("TurnOnEvent", sceneEvent.FirstTime, sceneEvent.ReplayTime);
            sceneEvent.Object.InvokeRepeating("TurnOffEvent", sceneEvent.FirstTime + sceneEvent.Duration, sceneEvent.ReplayTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class SceneEvent
{
    public Basic_Area Object;
    public float FirstTime;
    public float ReplayTime;
    public float Duration;
}
