using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class ButtonWithCooldown : MonoBehaviour
  {
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Cooldown _cooldown;
    public Button Button => _button;
    public Cooldown Cooldown => _cooldown;
  }
}