using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField]
    public float speed = 1;
    [SerializeField]
    float a = 0.01f;
    [SerializeField]
    public float SpeedLimit = 0.01f;

    public bool Way = true;
    bool S = false;
    public bool End = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, (int)Random.Range(0,4) * 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (End)
        {
            speed *= 0.93f;
        }
        if (!S) return;
        if (Way)
        {
            transform.eulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles -= new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (speed < SpeedLimit)
        {
            speed += a * Time.deltaTime;
        }
        else
        {
            speed += a / 2 * Time.deltaTime;
        }
    }

    public void GO()
    {
        S = true;
    }
    public void Stop()
    {
        End = true;
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(3);
        S = true;
    }
}
