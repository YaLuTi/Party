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
        if (Follow == null) return;

        float OldRange = (1.5f - 0f);
        float NewRange = (1 - 0);
        float NewValue = (((Follow.transform.localScale.x - 0f) * NewRange) / OldRange) + 0;
        transform.localScale = new Vector3(NewValue, NewValue, transform.localScale.z);


        Vector3 v = Follow.transform.position;
        if (!Y_Follow)
        {
            v.y = transform.position.y;
        }
        transform.position = v;
    }
}
