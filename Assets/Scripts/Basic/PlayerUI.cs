using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerHitten playerHitten;
    public Slider slider;
    
    public Transform follow;
    public float y = 1.8f;
    // Start is called before the first frame update
    void Start()
    {
        if(playerHitten != null)
        {
            playerHitten.OnHealthChanged += OnPlayerHealthChanged;
            slider.maxValue = playerHitten.GetMaxHealth();
            slider.value = slider.maxValue;
        }
    }

    public void SetUp(PlayerHitten _playerHitten)
    {
        playerHitten = _playerHitten;
        follow = playerHitten.playerMove.transform;
        _playerHitten.OnHealthChanged += OnPlayerHealthChanged;
        slider.maxValue = _playerHitten.GetMaxHealth();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Camera.main.WorldToScreenPoint(follow.position + new Vector3(0, y, 0));
    }

    void OnPlayerHealthChanged(PlayerHitten playerHitten, float oldHP, float newHP)
    {
        slider.value = newHP;
    }
}
