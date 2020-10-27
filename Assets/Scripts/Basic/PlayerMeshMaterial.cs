using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshMaterial : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    SkinnedMeshRenderer InstanceRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        this.gameObject.AddComponent(typeof(SkinnedMeshRenderer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
