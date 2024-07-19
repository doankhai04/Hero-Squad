using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    Animator animator;
    DamageDealer damageDealer;
    BoxCollider2D boxCollider;
    GameSession scoreManagement;
    Hero hero;

    [SerializeField][Range(0, 200)] int bloodRemain = 100;
    [SerializeField] bool isHero;
    [SerializeField] float deathTime = 2f;
    [SerializeField] int enemyScore;
    public bool isDie = false;
    float animationRuntime;
    float animationLength;
    bool isHurting;
    GameSession gameSession;
    int originalBlood;
    private void Awake()
    {
        scoreManagement = FindObjectOfType<GameSession>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        hero = GetComponent<Hero>();
        if (isHero)
        {
            bloodRemain = HeroHealthKeeper.instance.GetHealth();
            originalBlood = bloodRemain;
        }
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (gameObject.tag == "Hero")
        {
            bloodRemain = HeroHealthKeeper.instance.heroHealth;

        }
    }
    public int GetBlood()
    {
        return bloodRemain;
    }

    public void TakeDamage(int damage)
    {
        if (!isDie)
        {
            bloodRemain -= damage;
            if (isHero)
            {
                HeroHealthKeeper.instance.SetHealth(bloodRemain);
            }
            if (bloodRemain < 1)
            {
                Die();
            }
        }
    }


    public void Die()
    {

        isDie = true;
        animator.SetTrigger("deathTrigger");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        if (!isHero)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameSession.instance.AddScore(enemyScore);
            AudioManagement.instance.PlayMonsterDeathAudio();
        }
        else
        {
            GameUI.instance.LoseEndText();
            GameUI.instance.EndGame();
            AudioManagement.instance.PlayHeroDeathAudio();
        }

        StartCoroutine(Destroy());

    }
    IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(deathTime);
        Destroy(gameObject);
    }

    public void ResetBlood()
    {
        bloodRemain = originalBlood;
        HeroHealthKeeper.instance.SetHealth(bloodRemain);
    }
}
