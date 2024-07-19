using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;
    Enemy enemy;
    [SerializeField] float idleTime = 0.5f;
    int attackIndex;
    private void Start()
    {

        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    public void BossAttack()
    {
        if (!gameObject.GetComponent<Health>().isDie)
        {
            attackIndex = Random.Range(0, 2);
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        animator.SetTrigger("Attack" + attackIndex);
        yield return new WaitForSecondsRealtime(idleTime);
        animator.SetBool("isIdle", true);
        WaitTime();
    }
    void WaitTime()
    {
        StartCoroutine(NextAttack());
    }
    IEnumerator NextAttack()
    {
        yield return new WaitForSecondsRealtime(idleTime);
        if (enemy.canAttack)
        {
            BossAttack();
        }
        else
        {
            enemy.UpdateWalkSpeed();
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", true);
        }
    }
}
