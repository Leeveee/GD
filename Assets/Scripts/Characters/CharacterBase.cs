using UnityEngine;

namespace Characters
{
  public abstract class CharacterBase : MonoBehaviour
  { 
    [SerializeField]
    protected Animator _animatorController;
    protected abstract float Hp { get; set; }
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