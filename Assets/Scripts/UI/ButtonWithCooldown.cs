using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class ButtonWithCooldown : MonoBehaviour
  {
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Cooldown.Cooldown _cooldown;
    public Button Button => _button;
    public Cooldown.Cooldown Cooldown => _cooldown;
  }
}