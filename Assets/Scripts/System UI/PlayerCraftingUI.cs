using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCraftingUI : MonoBehaviour
{
    Transform[] UIArray;
    RectTransform[] LeftArrowArray;
    RectTransform[] RightArrowArray;
    int Choosing = 0;
    int UILength = 0;

    // Start is called before the first frame update
    void Start()
    {
        UILength = transform.childCount;
        UIArray = new Transform[UILength];
        LeftArrowArray = new RectTransform[UILength - 1];
        RightArrowArray = new RectTransform[UILength - 1];


        for (int i = 0; i < transform.childCount; i++)
        {
            UIArray[i] = transform.GetChild(i);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            RightArrowArray[i] = UIArray[i].GetChild(1).GetComponent<RectTransform>();
            LeftArrowArray[i] =  UIArray[i].GetChild(0).GetComponent<RectTransform>();
        }
    }

    public void Right(string name)
    {
        UIArray[Choosing].GetComponentInChildren<TextMeshProUGUI>().text = name;
        RightArrowArray[Choosing].DOComplete();
        RightArrowArray[Choosing].DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), 0.5f, 2, 0.1f);
    }

    public void Left(string name)
    {
        UIArray[Choosing].GetComponentInChildren<TextMeshProUGUI>().text = name;
        LeftArrowArray[Choosing].DOComplete();
        LeftArrowArray[Choosing].DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), 0.5f, 2, 0.1f);
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
