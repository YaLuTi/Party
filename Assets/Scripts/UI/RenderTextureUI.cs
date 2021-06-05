using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureUI : MonoBehaviour
{
    Image image;
    public int num;
    public Sprite ss;

    [SerializeField]
    Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        ss = Sprite.Create(StageManager.playerIcons[num], new Rect(0, 0, StageManager.playerIcons[num].width, StageManager.playerIcons[num].height), Vector2.zero);
        GetComponent<Image>().sprite = ss;
        yield return null;
    }
}
