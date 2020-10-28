using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_SceneItem : MonoBehaviour
{
    // These's all for testing

    public GameObject Bullet;
    public Transform Spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger()
    {
        Instantiate(Bullet, Spawn.position, Quaternion.Euler(90,0,0));
    }
}
