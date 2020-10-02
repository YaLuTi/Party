using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUp(PlayerHitten playerHitten)
    {
        playerHitten.OnHealthChanged += OnPlayerHealthChanged;
        slider.maxValue = playerHitten.GetMaxHealth();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerHealthChanged(PlayerHitten playerHitten, float oldHP, float newHP)
    {
        slider.value = newHP;
    }
}
