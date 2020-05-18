using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public Animation CameraAnimtion;
    int AnimationCount = 0;
    List<AnimationState> animationStates = new List<AnimationState>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(AnimationState state in CameraAnimtion)
        {
            animationStates.Add(state);
        }
        CameraAnimtion.Play(animationStates[AnimationCount].name);
        AnimationCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraAnimtion.isPlaying && AnimationCount < animationStates.Count)
        {
            CameraAnimtion.Play(animationStates[AnimationCount].name);
            AnimationCount++;
        }
    }
}
