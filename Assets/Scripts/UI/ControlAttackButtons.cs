using System.Collections;
using UnityEngine;

namespace UI
{
  public class ControlAttackButtons : MonoBehaviour
  {
    [SerializeField]
    private ButtonWithCooldown _normalAttack;
    [SerializeField]
    private ButtonWithCooldown _superAttack;

    private void Awake()
    {
      AddListeners();
    }

    private void OnDestroy()
    {
      RemoveListeners();
    }

    private void AddListeners()
    {
      _normalAttack.Button.onClick.AddListener(NormalAttack);
      _superAttack.Button.onClick.AddListener(SuperAttack);
    }

    private void RemoveListeners()
    {
      _normalAttack.Button.onClick.RemoveListener(NormalAttack);
      _superAttack.Button.onClick.RemoveListener(SuperAttack);
    }

    private void SuperAttack() => ButtonClick(_superAttack);

    private void NormalAttack()
    {
      ButtonClick(_normalAttack);
      
    }

    private void ButtonClick (ButtonWithCooldown button)
    {
      if (button.Cooldown.IsCooldown)
      {
        return;
      }

      button.Cooldown.StartCooldown();
      button.Cooldown.CooldownImage.fillAmount = 1;
      StartCoroutine(UpdateCooldownFill(button.Cooldown));

      if (button.Cooldown.CooldownImage.fillAmount <= 0)
      {
        StopCoroutine(UpdateCooldownFill(button.Cooldown));
      }
    }

    private IEnumerator UpdateCooldownFill (Cooldown buttonCooldown)
    {
      while (buttonCooldown.CooldownImage.fillAmount > 0)
      {
        buttonCooldown.CooldownImage.fillAmount -= 1.0f / buttonCooldown.CooldownTime * Time.deltaTime;
        yield return null;
      }
    }
  }
}