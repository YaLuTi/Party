using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static int[] scores = new int[4];
    static Text[] texts;

    // Start is called before the first frame update
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddScore(int id, int score)
    {
        scores[id] += score;
        for(int i =0; i < texts.Length; i++)
        {
            texts[i].text = scores[i].ToString();
        }
    }
}
