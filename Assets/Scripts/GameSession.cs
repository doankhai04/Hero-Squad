using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int playerScore = 0;
    int key = 0;
    [SerializeField] int blood;
    public int keys;
    public static GameSession instance;

    void Awake()
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
        keys = GameObject.FindGameObjectsWithTag("Keys").Length;
    }
    public void SetKeys()
    {
        keys = GameObject.FindGameObjectsWithTag("Keys").Length;
    }
    public int GetKeys()
    {
        return keys;
    }
    public int GetScore()
    {
        return playerScore;

    }

    public int GetKey()
    {
        return key;
    }
    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;

    }
    public void AddKey()
    {
        key += 1;
    }
    public void Reset()
    {
        playerScore = 0;
        key = 0;
    }
    public void ResetKey()
    {
        key = 0;
    }

    public bool OpenGate()
    {
        if (key == 4)
        {
            return true;
        }
        return false;

    }
    public void RecoverBlood()
    {
        HeroHealthKeeper.instance.Recover(blood);
    }
}
