using System.Collections.Generic;
using Characters;
using Data;
using UnityEngine;
public class SceneManager : MonoBehaviour
{
  public static SceneManager Instance;
  [SerializeField]
  private LevelConfig Config;

  public Player Player;
  public List<Enemy> Enemies;
  public GameObject Lose;
  public GameObject Win;

  private int currWave;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    SpawnWave();
  }

  public void AddEnemy (Enemy enemy)
  {
    Enemies.Add(enemy);
  }

  public void RemoveEnemy (Enemy enemy)
  {
    Enemies.Remove(enemy);

    if (Enemies.Count == 0)
    {
      SpawnWave();
    }
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
    if (currWave >= Config.Waves.Length)
    {
      Win.SetActive(true);
      return;
    }

    Wave wave = Config.Waves[currWave];

    foreach (GameObject character in wave.Characters)
    {
      Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
      Instantiate(character, pos, Quaternion.identity);
    }

    currWave++;
  }
}