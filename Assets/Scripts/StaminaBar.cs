using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Image barImage;
    public StaminaSystem staminaSystem;
    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        staminaSystem = new StaminaSystem();
    }

    private void Update()
    {
        staminaSystem.Update();
        barImage.fillAmount = staminaSystem.GetStaminaNormalized();
    }
}