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
        player.PickEvent -= OnPick;
        Debug.Log("!?");
    }

    public void SetUp(PlayerBehavior _player)
    {
        Debug.Log("A");
        player = _player;
        player.PickEvent += OnPick;
        player.ChargeEvent += OnCharge;
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
    }

    void OnPick(ItemBasic item)
    {
        for (int i = 0; i < images.Length; i++)
        {
            Destroy(images[i]);
        }
        int num = (int)item.MaxDurability;
        images = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            images[i] = Instantiate(Demo);
            images[i].transform.parent = DurabilityGroup.transform;
            images[i].transform.localScale = new Vector3(1, 1, 1);
        }
        for (int i = images.Length - 1; i > item.Durability - 1; i--)
        {
            images[i].GetComponent<Image>().enabled = false;
        }
        item.TriggerEvent += OnTrigger;
    }

    void OnTrigger(int d)
    {
        for (int i = images.Length - 1; i > d - 1; i--)
        {
            images[i].GetComponent<Image>().enabled = false;
        }
    }

    void OnCharge(float v)
    {
        slider.value = v;
    }

    
}
