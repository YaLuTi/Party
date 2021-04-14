using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicChange : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] bgms;

    Coroutine coroutine;

    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBGM(int i)
    {
        num += i;
        num = (num >= bgms.Length) ? 0 : num;
        num = (num < 0) ? bgms.Length - 1 : num;
        coroutine = StartCoroutine(_ChangeBGM());
    }

    IEnumerator _ChangeBGM()
    {
        audioSource.DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.3f);
        audioSource.clip = bgms[num];
        audioSource.Play();
        audioSource.DOFade(0.113f, 0.1f);
        yield return null;
    }
}
