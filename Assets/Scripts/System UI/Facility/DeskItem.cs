using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskItem : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material highLight;
    Material instanceHighLight;

    public string ItemName = "ItemName";
    [TextArea]
    public string ItemDescription = "ItemDescription";
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Material[] mats = new Material[2];
        mats[0] = meshRenderer.materials[0];
        mats[1] = highLight;
        meshRenderer.materials = mats;
        instanceHighLight = meshRenderer.materials[1];
        instanceHighLight.SetFloat("_Fresnel_Multiplier", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choose()
    {
        instanceHighLight.SetFloat("_Fresnel_Multiplier", 1);
    }

    public void Cancel()
    {
        instanceHighLight.SetFloat("_Fresnel_Multiplier", 0);
    }
}
