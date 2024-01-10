using UnityEngine;

namespace Data
{
  [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
  public class Wave : ScriptableObject
  {
    public GameObject [] Characters;
  }
}