using System;
using UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const float ROTATE_SPEED = 1000f;

    [SerializeField]
    public CharacterController controller;
    public float Hp;
    public float Damage;
    public float AttackSpeed;
    public float AttackRange = 2;
    private float lastAttackTime = 0;
    private bool isDead = false;
    public Animator AnimatorController;
    public float moveSpeed = 5f;
    private ControlAttackButtons _controlAttackButtons;
    private Enemy closestEnemy = null;

    private void Awake()
    {
        _controlAttackButtons = ControlAttackButtons.Instance;
        _controlAttackButtons.NormalAttack += NormalAttack;
        _controlAttackButtons.SuperAttack += SuperAttack;
    }

    private void SuperAttack()
    {
        //  throw new NotImplementedException();
    }

    private void NormalAttack()
    {
        AnimatorController.SetTrigger(AnimatorHash.Attack);

        if (closestEnemy != null)
        {
            var distance = Vector3.Distance(transform.position, closestEnemy.transform.position);

            if (distance <= AttackRange)
            {
                transform.transform.rotation = Quaternion.LookRotation(closestEnemy.transform.position - transform.position);
                closestEnemy.Hp -= Damage;
            }
        }
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

        var enemies = SceneManager.Instance.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];

            if (enemy == null)
            {
                continue;
            }

            if (closestEnemy == null)
            {
                closestEnemy = enemy;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
            }

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

        AnimatorController.SetFloat(AnimatorHash.Walk, moveDirection.magnitude);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger(AnimatorHash.Die);

        SceneManager.Instance.GameOver();
    }
}