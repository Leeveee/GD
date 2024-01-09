using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  [Serializable]
  public class Cooldown
  {
    [SerializeField]
    private Image _cooldownImage;
    [SerializeField]
    private float _cooldownTime;
    private float _starCooldownTime;

    public float CooldownTime => _cooldownTime;
    public Image CooldownImage => _cooldownImage;
    public bool IsCooldown => Time.time < _starCooldownTime;

    public void StartCooldown() => _starCooldownTime = Time.time + _cooldownTime;
  }
}