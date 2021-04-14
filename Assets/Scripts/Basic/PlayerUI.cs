using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerHitten playerHitten;
    public Slider slider;

    public Image Fill;

    public Transform follow;
    public float y = 1.8f;

    [SerializeField]
    Color FullHealth;
    [SerializeField]
    Color HalfHealth;
    [SerializeField]
    Color LowHealth;

    // Start is called before the first frame update
    void Start()
    {
        if(playerHitten != null)
        {
            // playerHitten.OnHealthChanged += this.OnPlayerHealthChanged;
            slider.maxValue = playerHitten.GetMaxHealth();
            slider.value = slider.maxValue;
        }
        Fill.color = FullHealth;
    }

    private void OnDestroy()
    {
        Debug.Log("!?");
        playerHitten.OnHealthChanged -= this.OnPlayerHealthChanged;
    }

    public void SetUp(PlayerHitten _playerHitten)
    {
        Debug.Log("A");
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
        if(slider.normalizedValue >= 0.5f)
        {
            float OldRange = (1 - 0.5f);
            float NewRange = (1 - 0);
            float NewValue = (((slider.normalizedValue - 0.5f) * NewRange) / OldRange) + 0;
            Fill.color = Color.Lerp(HalfHealth, FullHealth, NewValue);
        }
        else
        {
            float OldRange = (0.5f - 0);
            float NewRange = (1 - 0f);
            float NewValue = (((slider.normalizedValue - 0) * NewRange) / OldRange) - 0.8f;
            Fill.color = Color.Lerp(LowHealth, HalfHealth, NewValue);
        }
    }
}
