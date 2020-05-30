using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Follow : MonoBehaviour
{
    public GameObject Follow;
    public bool Y_Follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Follow.transform.position;
        if (!Y_Follow)
        {
            v.y = transform.position.y;
        }
        transform.position = v;
    }
}
