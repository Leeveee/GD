using System;
using UnityEngine;
using UnityEngine.UI;

namespace Cooldown
{
  [Serializable]
  public class Cooldown
  {
    [SerializeField]
    private Image _cooldownImage;
    [SerializeField]
    private float _cooldownTime;
    [SerializeField]
    private CooldownWithAnimationClip _cooldownWithAnimationClip;

    private float _starCooldownTime;

    public Image CooldownImage => _cooldownImage;
    public bool IsCooldown => Time.time < _starCooldownTime;

    public void StartCooldown() => _starCooldownTime = Time.time + _cooldownTime;

    public float CooldownTime
    {
      get
      {
        if (_cooldownWithAnimationClip.IsAnimationClip)
          _cooldownTime = _cooldownWithAnimationClip.AnimationClipLength;

        return _cooldownTime;
      }
    }
  }
}