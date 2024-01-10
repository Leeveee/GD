using System;
using UnityEngine;

namespace Cooldown
{
  [Serializable]
  public class CooldownWithAnimationClip
  { 
    [SerializeField]
    private bool _isSetAnimationClipForCooldown;
    [SerializeField]
    private AnimationClip _animationClip;
    public bool IsAnimationClip => _isSetAnimationClipForCooldown && _animationClip != null;
     
    public float AnimationClipLength => _animationClip.length;
  }
}