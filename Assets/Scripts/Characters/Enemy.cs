using Data;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
  public class Enemy : CharacterBase
  {
    [SerializeField]
    private EnemyData _enemyData;
    [SerializeField]
    private NavMeshAgent _agent;

    private SceneManager _sceneManager;

    private bool isDead;
    private float _hp;
    private float lastAttackTime;
    protected override float Hp
    {
      get
      {
        return _hp;
      }
      set
      {
        _hp = Mathf.Clamp(value, 0f, _enemyData.Hp);
      }
    }

    private void Awake()
    {
      _sceneManager = SceneManager.Instance;
    }

    private void Start()
    {
      _sceneManager.AddEnemy(this);
      _agent.SetDestination(_sceneManager.Player.transform.position);
      _hp = _enemyData.Hp;
    }

    private void Update()
    {
      if (isDead)
      {
        return;
      }

      if (Hp <= 0)
      {
        Die();
        _agent.isStopped = true;
        return;
      }

      float distance = Vector3.Distance(transform.position, _sceneManager.Player.transform.position);

      if (distance <= _enemyData.AttackRange)
      {
        _agent.isStopped = true;

        if (Time.time - lastAttackTime > _enemyData.AttackSpeed)
        {
          lastAttackTime = Time.time;
          _sceneManager.Player.TakeDamage(_enemyData.Damage);
          _animatorController.SetTrigger(AnimatorHash.Attack);
        }
      }
      else
      {
        _agent.SetDestination(_sceneManager.Player.transform.position);
      }

      _animatorController.SetFloat("Speed", _agent.speed);
      //   Debug.Log(Agent.speed);
    }

    protected override void Die()
    {
      _sceneManager.RemoveEnemy(this);
      isDead = true;
      _animatorController.SetTrigger(AnimatorHash.Die);
    }
  }
}