using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Area : MonoBehaviour
{
    bool IsWorking = false;
    public ParticleSystem PrepareParticle;
    public ParticleSystem WoringParticle;

    public int PlayerID = -1;
    List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0)) return;
        // playerHittens.Add(other.transform.root.GetComponent<PlayerHitten>());
    }

    private void OnTriggerExit(Collider other)
    {
        
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (!IsWorking) return;
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0)) return;
        other.transform.root.GetComponent<PlayerHitten>().OnDamaged(0.015f, PlayerID);
    }

    public void TurnOnEvent()
    {
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        PrepareParticle.Play();
        yield return new WaitForSeconds(2.5f);
        PrepareParticle.Stop();
        WoringParticle.Play();
        yield return new WaitForSeconds(1f);
        IsWorking = true;
        yield return null;
    }

    public void TurnOffEvent()
    {
        StartCoroutine(TurnOff());
    }
    IEnumerator TurnOff()
    {
        WoringParticle.Stop();
        yield return new WaitForSeconds(5f);
        IsWorking = false;
        yield return null;
    }
}
