using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProfileChooseUI : MonoBehaviour
{
    public TextMeshPro text;

    public Transform RightArrowArray;
    public Transform LeftArrowArray;

    public AudioSource UISound;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set(int num)
    {
        text.text = PlayerPrefs.GetString("Profile_" + num + "_Name");
    }

    public void Right(int num)
    {
        text.text = PlayerPrefs.GetString("Profile_" + num + "_Name");
        RightArrowArray.DOComplete();
        RightArrowArray.DOPunchScale(new Vector3(.3f, .3f, .3f), 0.3f, 2, 0.1f);
        UISound.PlayOneShot(UISound.clip);
    }

    public void Left(int num)
    {
        text.text = PlayerPrefs.GetString("Profile_" + num + "_Name");
        LeftArrowArray.DOComplete();
        LeftArrowArray.DOPunchScale(new Vector3(.3f, .3f, .3f), 0.3f, 2, 0.1f);
        UISound.PlayOneShot(UISound.clip);
    }

    public void Ready()
    {
        animator.SetTrigger("Play");
    }
    public void UnReady()
    {
        animator.SetTrigger("Re");
    }
}
