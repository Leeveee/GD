using System;
using System.Collections;
using UnityEngine;

namespace UI
{
  public class ControlAttackButtons : MonoBehaviour
  {
    public static ControlAttackButtons Instance;
    public event Action SuperAttack;
    public event Action NormalAttack;
    
    [SerializeField]
    private ButtonWithCooldown _normalAttack;
    [SerializeField]
    private ButtonWithCooldown _superAttack;
    
    
    private void Awake()
    {
      Instance = this;
      
      AddListeners();
    }

    private void OnDestroy()
    {
      RemoveListeners();
    }

    private void AddListeners()
    {
      _normalAttack.Button.onClick.AddListener(NormalAttackClick);
      _superAttack.Button.onClick.AddListener(SuperAttackClick);
    }

    private void RemoveListeners()
    {
      _normalAttack.Button.onClick.RemoveListener(NormalAttackClick);
      _superAttack.Button.onClick.RemoveListener(SuperAttackClick);
    }

    private void SuperAttackClick() => ButtonClick(_superAttack,SuperAttack);

    private void NormalAttackClick()
    {
      ButtonClick(_normalAttack,NormalAttack);
      
    }

    private void ButtonClick (ButtonWithCooldown button, Action attack)
    {
      if (button.Cooldown.IsCooldown)
      {
        return;
      }
      
      attack?.Invoke();
      button.Cooldown.StartCooldown();
      button.Cooldown.CooldownImage.fillAmount = 1;
      StartCoroutine(UpdateCooldownFill(button.Cooldown));

      if (button.Cooldown.CooldownImage.fillAmount <= 0)
      {
        StopCoroutine(UpdateCooldownFill(button.Cooldown));
      }
    }

    private IEnumerator UpdateCooldownFill (Cooldown.Cooldown buttonCooldown)
    {
      while (buttonCooldown.CooldownImage.fillAmount > 0)
      {
        buttonCooldown.CooldownImage.fillAmount -= 1.0f / buttonCooldown.CooldownTime * Time.deltaTime;
        yield return null;
      }
    }
  }
}