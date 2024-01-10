using System;
using System.Collections.Generic;
using Characters;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;
public class SceneManager : MonoBehaviour
{
  public static SceneManager Instance;

  public event Action<int> OnUpdateCountWaves;
  public event Action OnUpdateCountEnemyOnWave;
  
  public Player Player;
  public List<Enemy> Enemies;
  public GameObject Lose;
  public GameObject Win;

  [SerializeField]
  private LevelConfig Config;

  private int currWave;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  private void Start()
  {
    SpawnWave();
  }

  public void AddEnemy (Enemy enemy)
  {
    Enemies.Add(enemy);
    OnUpdateCountEnemyOnWave?.Invoke();
  }

  public void RemoveEnemy (Enemy enemy)
  {
    Enemies.Remove(enemy);

    if (Enemies.Count == 0)
    {
      SpawnWave();
    }

    OnUpdateCountEnemyOnWave?.Invoke();
  }

  public void GameOver()
  {
    Lose.SetActive(true);
  }

  public void Reset()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
  }

  private void SpawnWave()
  {
    if (currWave >= Config.Waves.Length && Enemies.Count==0)
    {
      Win.SetActive(true);
      return;
    }

    Wave wave = Config.Waves[currWave];

    foreach (Enemy character in wave.Enemies)
    {
      Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
      Instantiate(character, pos, Quaternion.identity);
    }

    currWave++;
    int countActiveWave = Config.Waves.Length - currWave;
    OnUpdateCountWaves?.Invoke(countActiveWave);
  }
}