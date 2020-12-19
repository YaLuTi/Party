using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightControll : MonoBehaviour
{
    public GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().OnPlayerJoin += Light;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().OnPlayerJoin -= Light;
    }

    void Light(GameObject player, int n)
    {
        lights[n].SetActive(true);
    }
}
