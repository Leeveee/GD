using System.Collections.Generic;
using Data;
using UI;
using UnityEngine;
namespace Characters
{
  public class Player : CharacterBase
  {
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const float ROTATE_SPEED = 1000f;
    private const int DAMAGE_INCREASE_FACTOR = 2;

    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private PlayerData _playerData;

    private ControlAttackButtons _controlAttackButtons;
    private Enemy closestEnemy;
    private bool isDead;
    private float _hp;

    protected override float Hp
    {
      get => _hp;
      set => _hp = Mathf.Clamp(value, 0f, _playerData.Hp);
    }

    private void Awake()
    {
      _controlAttackButtons = ControlAttackButtons.Instance;
      _hp = _playerData.Hp;
      AddListeners();
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
        return;
      }

      Move();

      List<Enemy> enemies = SceneManager.Instance.Enemies;

      for (int i = 0; i < enemies.Count; i++)
      {
        Enemy enemy = enemies[i];

        if (enemy == null)
        {
          continue;
        }

        if (closestEnemy == null)
        {
          closestEnemy = enemy;
          continue;
        }

        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        float closestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        if (distance < closestDistance)
        {
          closestEnemy = enemy;
        }
      }
    
      _controlAttackButtons.ActivenessButton(_controlAttackButtons.SuperAttackComponent, !(DistanceToTarget() >= _playerData.AttackRange));
    
    }

    private void OnDestroy()
    {
      RemoveListeners();
    }

    private void AddListeners()
    {
      _controlAttackButtons.NormalAttack += NormalAttack;
      _controlAttackButtons.SuperAttack += SuperAttack;
    }

    private void RemoveListeners()
    {
      _controlAttackButtons.NormalAttack -= NormalAttack;
      _controlAttackButtons.SuperAttack -= SuperAttack;
    }

    protected override void Die()
    {
      isDead = true;
      _animatorController.SetTrigger(AnimatorHash.Die);

      SceneManager.Instance.GameOver();
    }

    private void SuperAttack()
    {
      if (isDead)
      {
        return;
      }

      _animatorController.SetTrigger(AnimatorHash.SuperAttack);

      if (DistanceToTarget() <= _playerData.AttackRange)
      {
        transform.transform.rotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
        closestEnemy.TakeDamage(_playerData.Damage * DAMAGE_INCREASE_FACTOR);
      }
    }

    private void NormalAttack()
    {
      if (isDead)
      {
        return;
      }

      _animatorController.SetTrigger(AnimatorHash.Attack);

      if (DistanceToTarget() <= _playerData.AttackRange)
      {
        transform.transform.rotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
        closestEnemy.TakeDamage(_playerData.Damage);
      }
    }

    private void Move()
    {
      float horizontal = Input.GetAxis(HORIZONTAL);
      float vertical = Input.GetAxis(VERTICAL);

      Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

      if (moveDirection != Vector3.zero)
      {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * ROTATE_SPEED);
      }

      _animatorController.SetFloat(AnimatorHash.Walk, moveDirection.magnitude);
      _characterController.Move(moveDirection * _playerData.MoveSpeed * Time.deltaTime);
    }

    private float DistanceToTarget()
    {
      if (closestEnemy != null)
      {
        return Vector3.Distance(transform.position, closestEnemy.transform.position);
      }

      return default;
    }
  }
}