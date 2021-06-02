using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCreating : MonoBehaviour
{
    [SerializeField]
    int num;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<PlayerCreating>().CreatPhoto(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
