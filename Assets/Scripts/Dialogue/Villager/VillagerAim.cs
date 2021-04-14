using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAim : MonoBehaviour
{
    public Transform aimController;
    public Transform aimTarget;

    void Update()
    {
        float weight = (aimTarget == null) ? 0 : 1f;
        Vector3 pos = (aimTarget == null) ? transform.position + transform.forward + Vector3.up : aimTarget.position + Vector3.up;
        
        aimController.position = Vector3.Lerp(aimController.position, pos, .3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            aimTarget = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            aimTarget = null;
    }

}
