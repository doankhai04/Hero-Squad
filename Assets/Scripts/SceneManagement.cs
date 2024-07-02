using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    public static SceneManagement instance;
    GameSession gameSession;
    Health heroHealth;

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
    private void Start()
    {
        if (!(SceneManager.GetActiveScene().name == "Menu"))
        {
            heroHealth = FindObjectOfType<Hero>().GetComponent<Health>();

        }
        gameSession = FindObjectOfType<GameSession>();
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GameOver()
    {
        StartCoroutine(WaitTime("GameOver", delayTime));
    }
    IEnumerator WaitTime(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ResetScene()
    {
        if (!(SceneManager.GetActiveScene().name == "Menu"))
        {

            if (heroHealth.isDie)
            {
                heroHealth.ResetBlood();
                gameSession.Reset();
            }
        }

        Destroy(gameObject);
    }

}
