using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTMPAnimate : MonoBehaviour
{
    TMP_Animated tMP;
    // Start is called before the first frame update
    void Start()
    {
        tMP = GetComponent<TMP_Animated>();
        tMP.ReadText("TESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTESTTEST");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
