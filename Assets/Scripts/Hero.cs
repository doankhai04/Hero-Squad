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
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    bool IsMove(float move)
    {
        return (Mathf.Abs(move) > Mathf.Epsilon);
    }

    void Run()
    {
        Vector2 runVector = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = runVector;
        bool isMove = IsMove(rb.velocity.x);
        if (isMove)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
        animator.SetBool("isRunning", isMove);
    }
    void OnJump(InputValue value)
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            rb.velocity = new Vector2(0f, jumpSpeed);
        }

    }
    void OnAttack(InputValue value)
    {
        bool isAttack = value.isPressed;
        animator.SetBool("isAttacking",isAttack);
    }
}
