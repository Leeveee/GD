using UnityEngine;

namespace Characters
{
  public abstract class CharacterBase : MonoBehaviour
  { 
    [SerializeField]
    protected Animator _animatorController;
    public abstract float Hp { get; protected set; }
    protected abstract void Die();
    
    public void TakeDamage (int damage)
    {
      Hp -= damage;
    }
    
    public void TakeHeal (int heal)
    {
      Hp += heal;
    }
  }
}