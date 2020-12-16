using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item_Axe : Item_Melee
{
    public GameObject ChargeParticle;
    public GameObject PP;
    public override void Hold(Transform t)
    {
        transform.parent = t;
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

    public override void Enhance()
    {
    }

    IEnumerator BGMEvent()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
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
}
