using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDebug : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Cloth>().enabled = true;
    }
    private void Update()
    {
    }
}
