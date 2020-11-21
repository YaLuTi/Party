using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Area : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0)) return;
        // playerHittens.Add(other.transform.root.GetComponent<PlayerHitten>());
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0)) return;
        other.transform.root.GetComponent<PlayerHitten>().OnDamaged(0.015f, PlayerID);
    }
}
