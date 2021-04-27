using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float a = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;
        speed += a;
    }

}
