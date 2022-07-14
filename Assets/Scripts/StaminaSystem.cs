using UnityEngine;

public class StaminaSystem
{
  private const int STAMINA_MAX = 100;

  private float staminaAmount;
  private float staminaRegenAmount;

  public StaminaSystem()
  {
    staminaAmount = STAMINA_MAX;
    staminaRegenAmount = 20f;
  }

  public void Update()
  {
    staminaAmount += staminaRegenAmount * Time.deltaTime;
    staminaAmount = Mathf.Clamp(staminaAmount, 0f, STAMINA_MAX);
    HandleStamina();
  }

  private void HandleStamina()
  {
    if(Input.GetKey(KeyCode.LeftShift))
      TrySpendStamina(40);
  }

  private void TrySpendStamina(int amount)
  {
    if (staminaAmount > 0)
    {
      staminaAmount -= amount * Time.deltaTime * 2;
    }
  }

  public float GetStaminaNormalized()
  {
    return staminaAmount / STAMINA_MAX;
  }
}