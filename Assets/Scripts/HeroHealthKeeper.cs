using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealthKeeper : MonoBehaviour
{
    public static HeroHealthKeeper instance;
    public int heroHealth;
    public int originalHeroHealth;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetHealth(int health)
    {
        heroHealth = health;
    }
    public int GetHealth()
    {
        return heroHealth;
    }
    public void Recover(int blood)
    {
        heroHealth += blood;
    }
    public void SetOriginalHeroHealth(int originalHealth)
    {
        originalHeroHealth = originalHealth;
    }
    public int GetOriginalHeroHealth()
    {
        return originalHeroHealth;
    }
}
