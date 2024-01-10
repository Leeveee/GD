using UnityEngine;

namespace Data
{
  [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/BattleCampInfo")]
  public class LevelConfig : ScriptableObject
  {
    public Wave [] Waves;
  }
}