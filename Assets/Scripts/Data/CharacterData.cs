using UnityEngine;
namespace Data
{
  public abstract class CharacterBaseData : ScriptableObject
  {
    public int Hp;
    public int Damage;
    public float AttackSpeed;
    public float AttackRange;
  }
}