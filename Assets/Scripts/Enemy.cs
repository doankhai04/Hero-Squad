using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    Animator animator;
    Health heroHealth;
    DamageDealer damageDealer;
    Hero hero;
    Boss boss;
    [SerializeField] GameObject attackPoint;
    AnimatorStateInfo currentStateInfo;
    [SerializeField] float angle;
    [SerializeField] Vector2 size;
    [SerializeField] bool isBoss = false;

    Vector2 moveVector;
    Vector3 or;
    Collider2D heros;
    public float walkSpeed = 5f;

    public bool isMove;
    public float originalWalkSpeed;
    float maxValue;
    public bool canAttack = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        boss = GetComponent<Boss>();
        isMove = true;
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        originalWalkSpeed = walkSpeed;
        or = size;
    }


    void Update()
    {
        Move();
    }
    void Move()
    {

        moveVector = new Vector2(walkSpeed, rb.velocity.y);
        rb.velocity = moveVector;
        animator.SetBool("isWalking", isMove);
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Ground")
        {
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
            walkSpeed = -walkSpeed;
        }
        if (other.tag == "Hero")
        {
          UpdateWalkSpeed();
            canAttack = false;
            isMove = true;
            if (!isBoss)
            {
                animator.SetBool("isAttacking", false);
            }
            else
            {
                if (!canAttack)
                {

                    animator.SetBool("isIdle", true);
                    animator.SetBool("isWalking", true);
                    UpdateWalkSpeed();
                }

            }
            Move();
        }

    }
    public void UpdateWalkSpeed()
    {
        if (originalWalkSpeed > 0)
        {
            walkSpeed = Mathf.Sign(transform.localScale.x) * originalWalkSpeed;
        }
        else
        {
            walkSpeed = -Mathf.Sign(transform.localScale.x) * originalWalkSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        heroHealth = other.GetComponent<Health>();

        if (other.tag == "Hero")
        {
            if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
            {

                canAttack = true;
                isMove = false;
                walkSpeed = 0f;
                if (!isBoss)
                {
                    animator.SetBool("isAttacking", true);
                }
                else
                {
                    if (canAttack)
                    {
                        boss.BossAttack();
                    }
                }
                if (heroHealth.isDie)
                {
                    if (!isBoss)
                    {
                        animator.SetBool("isAttacking", false);
                    }
                }

            }
            else
            {
                originalWalkSpeed = -walkSpeed;
                transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
            }
        }

    }
    void EnemyAttack()
    {

        heros = Physics2D.OverlapBox(attackPoint.transform.position, size, angle);
        if (heros != null)
        {
            heroHealth.TakeDamage(damageDealer.GetDamage());
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackPoint.transform.position, size);

    }
}
