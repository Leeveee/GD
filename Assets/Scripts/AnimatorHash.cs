using UnityEngine;
public static class AnimatorHash
{
  public static readonly int Idle = Animator.StringToHash("Idle");
  public static readonly int Walk = Animator.StringToHash("Speed");
  public static readonly int Attack = Animator.StringToHash("Attack"); 
  public static readonly int Die = Animator.StringToHash("Die");
}