using Constants;
using TMPro;
using UnityEngine;

namespace UI
{
  public class InfoPanel : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _countWaves;
    [SerializeField]
    private TextMeshProUGUI _countEnemyOnWaves;
    [SerializeField]
    private TextMeshProUGUI _playerHP;
    
    private SceneManager _sceneManager;
    private void Start()
    {
      _sceneManager = SceneManager.Instance;
      AddListener();
      UpdateHP();
    }

    private void OnDestroy()
    {
      RemoveListener();
    }

    private void AddListener()
    {
      _sceneManager.OnUpdateCountWaves += UpdateCountWaves;
      _sceneManager.OnUpdateCountEnemyOnWave += UpdateEnemyCount;
      _sceneManager.Player.OnUpdateHP += UpdateHP;
    }

    private void RemoveListener()
    {
      _sceneManager.OnUpdateCountWaves -= UpdateCountWaves;
      _sceneManager.OnUpdateCountEnemyOnWave -= UpdateEnemyCount;
      _sceneManager.Player.OnUpdateHP -= UpdateHP;
    }

    private void UpdateEnemyCount()
    {
      _countEnemyOnWaves.text = $"{GameConstants.COUNT_ENEMY_ON_WAVE} {_sceneManager.Enemies.Count}";
    }

    private void UpdateCountWaves(int count)
    {
      _countWaves.text = $"{GameConstants.COUNT_WAVES} {count}";
    }

    private void UpdateHP()
    {
      _playerHP.text =  $"{GameConstants.HP} {_sceneManager.Player.Hp}";
    }
  }
}