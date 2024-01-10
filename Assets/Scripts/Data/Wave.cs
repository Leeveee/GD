using Characters;
using UnityEngine;

namespace Data
{
  [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
  public class Wave : ScriptableObject
  {
    public Enemy [] Enemies;
  }
}