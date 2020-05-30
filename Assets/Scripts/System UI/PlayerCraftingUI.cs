using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraftingUI : MonoBehaviour
{
    Transform[] UIArray;
    int Choosing = 0;
    int UILength = 0;

    // Start is called before the first frame update
    void Start()
    {
        UILength = transform.childCount;
        UIArray = new Transform[UILength];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            UIArray[i] = transform.GetChild(i);
        }
    }

    public void ChangeChoosing(int change)
    {
        Choosing += change;
        if(Choosing < 0)
        {
            Choosing = UILength - 1;
        }
        else if(Choosing >= UILength)
        {
            Choosing = 0;
        }
    }
}
