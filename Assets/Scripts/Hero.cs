using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveInput;
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsuleCollider;
    Health enemyHealth;
    DamageDealer damageDealer;
    Health health;
    bool isMove;
    bool isJump;
    float originalGravityScale;
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemiesLayerMask;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    bool isAttack;
    private void Awake()
    {
        animator = GetComponent<Animator>();


    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        damageDealer = GetComponent<DamageDealer>();
        originalGravityScale = rb.gravityScale;
        health = GetComponent<Health>();
    }


    void Update()
    {
        if (!gameObject.GetComponent<Health>().isDie)
        {
            Run();
            Jump();
            Climb();

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gate" && GameSession.instance.OpenGate())
        {

            GameUI.instance.NextScene();
        }
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            rb.velocity = new Vector2(0f, 0f);
            health.TakeDamage(200);
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 runVector = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = runVector;

        isMove = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (isMove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
        animator.SetBool("isRunning", isMove);
    }
    void OnJump(InputValue value)
    {
        bool isOnGround = boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!isOnGround) return;
        if (value.isPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

    }
    void Jump()
    {
        isJump = Mathf.Abs(rb.velocity.y) > 0.2f &&
                !capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) &&
                !boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        animator.SetBool("isJumping", isJump);
    }
    void OnAttack(InputValue value)
    {
        animator.SetBool("isAttacking", value.isPressed);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemiesLayerMask);

        if (enemies != null)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemyHealth = enemy.GetComponent<Health>();
            }
        }

    }
    void AttackEnemy()
    {
        enemyHealth?.TakeDamage(damageDealer.GetDamage());
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
    void Climb()
    {
        bool isTouchingClimbing = capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!isTouchingClimbing)
        {
            rb.gravityScale = originalGravityScale;
            boxCollider.isTrigger = false;
            capsuleCollider.isTrigger = false;

            animator.SetBool("isClimbing", false);

            return;
        }
        Vector2 climbVector = new Vector2(rb.velocity.x, climbSpeed * moveInput.y);
        rb.gravityScale = 0f;
        rb.velocity = climbVector;
        bool isClimbing = (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon) && isTouchingClimbing;
        animator.SetBool("isClimbing", isClimbing);

    }
}
