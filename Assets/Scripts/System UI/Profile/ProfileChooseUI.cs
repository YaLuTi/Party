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

    public void Set(string s)
    {
        text.text = s;
    }

    public void Right(string s)
    {
        text.text = s;
        RightArrowArray.DOComplete();
        RightArrowArray.DOPunchScale(new Vector3(.3f, .3f, .3f), 0.3f, 2, 0.1f);
        UISound.PlayOneShot(UISound.clip);
    }

    public void Left(string s)
    {
        text.text = s;
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
