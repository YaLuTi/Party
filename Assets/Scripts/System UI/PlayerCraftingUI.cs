using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCraftingUI : MonoBehaviour
{
    Transform[] UIArray;
    Transform[] LeftArrowArray;
    Transform[] RightArrowArray;
    int Choosing = 0;
    int UILength = 0;

    // Start is called before the first frame update
    void Start()
    {
        UILength = transform.childCount;
        UIArray = new Transform[UILength];
        LeftArrowArray = new Transform[UILength - 1];
        RightArrowArray = new Transform[UILength - 1];


        for (int i = 0; i < transform.childCount; i++)
        {
            UIArray[i] = transform.GetChild(i);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            RightArrowArray[i] = UIArray[i].GetChild(1).GetComponent<Transform>();
            LeftArrowArray[i] =  UIArray[i].GetChild(0).GetComponent<Transform>();
        }
    }

    public void Right(string name)
    {
        UIArray[Choosing].GetComponentInChildren<TextMeshPro>().text = name;
        RightArrowArray[Choosing].DOComplete();
        RightArrowArray[Choosing].DOPunchScale(new Vector3(.3f, .3f, .3f), 0.3f, 2, 0.1f);
    }

    public void Left(string name)
    {
        UIArray[Choosing].GetComponentInChildren<TextMeshPro>().text = name;
        LeftArrowArray[Choosing].DOComplete();
        LeftArrowArray[Choosing].DOPunchScale(new Vector3(.3f, .3f, .3f), 0.3f, 2, 0.1f);
    }

    /*public void ChangeChoosing(int change)
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
    }*/
}
