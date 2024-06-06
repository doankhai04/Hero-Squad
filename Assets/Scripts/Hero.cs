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

    bool isMove;
    bool isJump;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        Run();
        Jump();
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
        isJump = Mathf.Abs(rb.velocity.y) > 0.1f;
        animator.SetBool("isJumping", isJump);
    }
    void OnAttack(InputValue value)
    {
        bool isAttack = value.isPressed;
        animator.SetBool("isAttacking", isAttack);
    }
}
