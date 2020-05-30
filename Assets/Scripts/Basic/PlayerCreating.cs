using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreating : MonoBehaviour
{
    public PlayerCraftingUI PlayerCreatingUI;
    public List<GameObject> Hats = new List<GameObject>();
    int choosing = 0;

    // Start is called before the first frame update
    void Start()
    {
        Hats[0].SetActive(true);
    }

    void OnUI_Right()
    {
        Hats[choosing].SetActive(false);
        choosing++;
        if(choosing >= Hats.Count)
        {
            choosing = 0;
        }
        Hats[choosing].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
