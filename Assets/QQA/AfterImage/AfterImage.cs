using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public Animator myAnimator;
    //public CharacterControl myCharacterControl;
    public Renderer myRenderer;

    public Animator targetAnimator;
    public GameObject targetObject;

    public float time;
    public float intensity;
    public float pow;
    public float timeMax = 45;

    public bool active;

	// Use this for initialization
	void Start ()
    {
		//targetObject = 
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if (CitadelDeep.hitPause > 0 || CitadelDeep.debugPause) { return; }
        if (time > 0) { time--; active = true; intensity = (time / timeMax) * 10 * pow; UpdateRenderer(); }//transform.localScale *= 1.03f; }
        else { active = false; intensity = 0; }
	}

    void UpdateRenderer()
    {
        myRenderer.material.SetFloat("_Intensity", intensity);
        myRenderer.material.SetFloat("_MKGlowPower", intensity);
    }

    public void Activate()
    {
        active = true;
        transform.position = targetObject.transform.position;
        transform.localScale = targetObject.transform.lossyScale;
        transform.rotation = targetObject.transform.rotation;

        myAnimator.Play(targetAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, targetAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //myAnimator.Play(myCharacterControl.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //myAnimator.no

        foreach(AnimatorControllerParameter param in targetAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Float)
            {
                myAnimator.SetFloat(param.name, targetAnimator.GetFloat(param.name));
            }
            if (param.type == AnimatorControllerParameterType.Int)
            {
                myAnimator.SetInteger(param.name, targetAnimator.GetInteger(param.name));
            }
        }

        myAnimator.speed = 0;
        time = timeMax + 1;
        Update();
    }

}