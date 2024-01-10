using Constants;
using UnityEngine;

namespace Characters
{
  public class MegaEnemy : Enemy
  {
    [SerializeField]
    private Enemy enemyPrefab;

    protected override void Die()
    {
      base.Die();
      SpawnLittleEnemies();
    }

    private void SpawnLittleEnemies()
    {
      Vector3 spawnPosition = transform.position;

      for (int i = 0; i < GameConstants.COUNT_MONSTER_AFTER_KILL_MEGA_ENEMY; i++)
      {
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnPosition += Vector3.right;
      }
    }
  }
}