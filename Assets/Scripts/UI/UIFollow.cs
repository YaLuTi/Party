using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    [SerializeField]
    public Transform follow;
    [SerializeField]
    float y;
    [SerializeField]
    float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(follow.position + new Vector3(0, y, x));
    }
}
