using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDebug : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    private void Start()
    {
        Mesh m = GetComponent<MeshFilter>().mesh;
        m.bounds = new Bounds(Vector3.zero, Vector3.one * 2000);
    }
    private void Update()
    {
    }
}
