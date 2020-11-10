using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{

    //public CharacterControl myCharacterControl;
    public GameObject targetObject;     //Set these manually to the character object you're copying
    public Animator targetAnimator;   //Set these manually to the character object you're copying
    public GameObject prefab;           //This is the prefab you made in the scene. It's a parent transform with an animator and AfterImage script on it, with Armature and SkinnedMeshRenderer children
    public int poolSize = 10;
    public List<AfterImage> afterImages;

    public int interval = 10;

    public int time = 0;

    // Use this for initialization
    void Start()
    {
        //myCharacterControl = transform.root.GetComponent<CharacterControl>();
        //Debug.Log("START AFTER IMAGE POOL");
        afterImages = new List<AfterImage>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject nextAfterImage = Instantiate(prefab);
            afterImages.Add(nextAfterImage.GetComponent<AfterImage>());

            nextAfterImage.GetComponent<AfterImage>().targetObject = targetObject;      //Game Object Target
            nextAfterImage.GetComponent<AfterImage>().targetAnimator = targetAnimator;     //Animator Target
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (CitadelDeep.hitPause > 0 || CitadelDeep.debugPause) { return; }
        time++;
        if (time > interval) { time = 0; AddAfterImage(); }
    }

    void AddAfterImage()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!afterImages[i].active) { afterImages[i].Activate(); break; }
        }
    }
}