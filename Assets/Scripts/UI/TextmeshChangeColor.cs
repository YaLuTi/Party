using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextmeshChangeColor : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField]
    Color[] choose;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(int i)
    {
        text.color = choose[i];
    }
}
