using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityUI : MonoBehaviour
{

    public PlayerBehavior player;
    public Slider slider;

    public Image Fill;

    public RectTransform follow;
    public float y = 1.8f;

    public GameObject DurabilityGroup;
    GameObject[] images;
    public GameObject Demo;
    // Start is called before the first frame update
    void Start()
    {
        images = new GameObject[0];
    }

    private void OnDestroy()
    {
        Debug.Log("!?");
    }

    public void SetUp(PlayerBehavior _player)
    {
        Debug.Log("A");
        player = _player;
        /*slider.maxValue = playerHand.GetMaxHealth();
        slider.value = slider.maxValue;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(player.itemHand.HoldingItem == null)
        {
            for (int i = 0; i < images.Length; i++)
            {
                Destroy(images[i]);
            }
            slider.value = 0;
        }
        else
        {
            for(int i = 0; i < images.Length; i++)
            {
                Destroy(images[i]);
            }
            int num = (int)player.itemHand.HoldingItem.GetComponent<ItemBasic>().MaxDurability;
            images = new GameObject[num];
            for(int i = 0; i < num; i++)
            {
                images[i] = Instantiate(Demo);
                images[i].transform.parent = DurabilityGroup.transform;
            }
            for (int i = images.Length - 1; i > player.itemHand.HoldingItem.GetComponent<ItemBasic>().Durability - 1; i--)
            {
                images[i].GetComponent<Image>().enabled = false;
            }
            // slider.maxValue = playerHand.HoldingItem.GetComponent<ItemBasic>().d
        }
    }
}
