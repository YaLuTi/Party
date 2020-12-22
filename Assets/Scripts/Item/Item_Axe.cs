using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class Item_Axe : Item_Melee
{
    public GameObject ChargeParticle;
    public GameObject PP;

    float BGMVoice;
    float RemainTime = 15;
    float DRemainTime = 15;

    Transform tt;

    bool End = false;

    public override void Hold(Transform t)
    {
        transform.parent = t;
        tt = t;
        /*transform.localPosition = HoldedPosition;
        transform.localRotation = Quaternion.Euler(HoldedRotation);*/
        transform.DOLocalMove(HoldedPosition, 0.5f);
        transform.DOLocalRotate(HoldedRotation, 0.5f);
        IsHolded = true;
        if (GetComponentInParent<PlayerIdentity>())
        {
            PlayerID = GetComponentInParent<PlayerIdentity>().PlayerID;
        }

        transform.root.GetComponent<PlayerHitten>().AxeMode(ChargeParticle);
        StartCoroutine(BGMEvent());
    }

    public override void Update()
    {
        if (End) return;
        
        if (transform.position.y <= -30)
        {
            End = true;
            StartCoroutine(ReEvent());
        }

        if (IsHolded)
        {
            RemainTime -= Time.deltaTime;
            if(RemainTime < 0)
            {
                StartCoroutine(ReEvent());
                End = true;
            }
        }

        if(RemainTime != DRemainTime && !IsHolded)
        {
            StartCoroutine(ReEvent());
            End = true;
        }
    }

    public override void Enhance()
    {
    }

    public override void Throw()
    {
        base.Throw();
        End = true;
        StartCoroutine(ReEvent());
    }

    IEnumerator BGMEvent()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        BGMVoice = audioSource.volume;
        audioSource.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        /*float v = audioSource.volume;
        while(audioSource.volume >= 0)
        {
            audioSource.volume -= (v / 90);
        }*/
        GetComponent<AudioSource>().Play();
        PP.SetActive(true);
        yield return null;
    }

    IEnumerator ReEvent()
    {
        tt.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
        tt.root.GetComponentInChildren<PlayerHitten>().OutAxeMode();
        GetComponent<AudioSource>().DOFade(0, 1f);
        AudioSource audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioSource.DOFade(BGMVoice, 1.5f);
        yield return new WaitForSeconds(0.1f);
        PP.SetActive(false);
        transform.DOScale(0, 1.5f);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        yield return null;
    }
}
