using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public static GameUI instance;


    [Header("ButtonUI")]

    SceneManagement sceneManagement;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject mainScene;
    [SerializeField] GameObject mainImage;
    [SerializeField] GameObject playerScreen;
    [SerializeField] GameObject settingButton;
    [SerializeField] GameObject settingScreenBoard;
    [SerializeField] GameObject settingOptions;
    [SerializeField] GameObject audioSettingBoard;
    [SerializeField] GameObject finishBoard;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject backButtonMainScene;

    [SerializeField] float delayLoadScene = 0.6f;


    [Header("UIDisplay")]
    [Header("Blood")]
    [SerializeField] Slider bloodSlider;
    Health heroBlood;
    [Header("Coins")]
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Keys")]
    [SerializeField] Slider keySlider;
    [SerializeField] TextMeshProUGUI keysCollected;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI scoreEndingText;
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
        sceneManagement = FindObjectOfType<SceneManagement>();
        if (!(SceneManager.GetActiveScene().name == "Menu"))
        {
            heroBlood = FindObjectOfType<Hero>().GetComponent<Health>();
        }
    }
    private void Start()
    {
        keySlider.maxValue = 4;
        bloodSlider.maxValue = HeroHealthKeeper.instance.GetOriginalHeroHealth();
    }
    private void Update()
    {
        bloodSlider.value = HeroHealthKeeper.instance.GetHealth();
        keySlider.value = GameSession.instance.GetKey();
        scoreText.text = GameSession.instance.GetScore().ToString("000");
        scoreEndingText.text = GameSession.instance.GetScore().ToString();
        keysCollected.text = GameSession.instance.GetKey().ToString() + " / 4";
    }
    public void NextScene()
    {
        StartCoroutine(LoadNextScene());

    }

    IEnumerator LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            yield return new WaitForSecondsRealtime(delayLoadScene);
            SceneManagement.instance.ResetScene();
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1;
        }
        mainScene.SetActive(false);
        playerScreen.SetActive(true);
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            yield return new WaitForSecondsRealtime(delayLoadScene);
            WinEndText();
            EndGame();
            Time.timeScale = 0;
        }
        GameSession.instance.ResetKey();
    }
    public void WinEndText()
    {
        endText.text = "VICTORY";
    }
    public void LoseEndText()
    {
        endText.text = "LOSE";
    }
    public void PauseBoard()
    {
        settingScreenBoard.SetActive(true);
        settingOptions.SetActive(true);
        playerScreen.SetActive(false);
        audioSettingBoard.SetActive(false);
        Time.timeScale = 0;
    }
    public void BackToGame()
    {
        settingScreenBoard.SetActive(false);
        settingButton.SetActive(true);
        playerScreen.SetActive(true);
        Time.timeScale = 1;
    }

    public void AudioSetting()
    {
        settingOptions.SetActive(false);
        backButton.SetActive(true);
        backButtonMainScene.SetActive(false);
        audioSettingBoard.SetActive(true);
    }
    public void BackToResumeBoard()
    {
        settingOptions.SetActive(true);
        audioSettingBoard.SetActive(false);
    }

    public void ExitToMain()
    {
        SceneManagement.instance.ResetScene();
        SceneManager.LoadScene("Menu");
        HeroHealthKeeper.instance.SetHealth(200);
        GameSession.instance.Reset();
        bloodSlider.value = HeroHealthKeeper.instance.GetHealth();
        playerScreen.SetActive(false);
        finishBoard.SetActive(false);
        settingScreenBoard.SetActive(false);
        mainScene.SetActive(true);

    }
    public void EndGame()
    {
        StartCoroutine(EndGameScene());
    }
    IEnumerator EndGameScene()
    {
        yield return new WaitForSecondsRealtime(delayLoadScene);
        finishBoard.SetActive(true);
        playerScreen.SetActive(false);
        Time.timeScale = 0;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene_1_Forest");
        finishBoard.SetActive(false);
        playerScreen.SetActive(true);
        GameSession.instance.Reset();
        SceneManagement.instance.ResetScene();
        HeroHealthKeeper.instance.SetHealth(200);
        bloodSlider.value = HeroHealthKeeper.instance.GetHealth();
        Time.timeScale = 1;
    }
    public void AudioSettingMenu()
    {
        mainScene.SetActive(false);
        settingOptions.SetActive(false);
        finishBoard.SetActive(false);
        settingScreenBoard.SetActive(true);
        audioSettingBoard.SetActive(true);
        backButton.SetActive(false);
        backButtonMainScene.SetActive(true);
    }
    public void BackToMainScene()
    {
        HeroHealthKeeper.instance.SetHealth(200);
        GameSession.instance.Reset();
        SceneManagement.instance.ResetScene();
        SceneManager.LoadScene("Menu");
        mainScene.SetActive(true);
        audioSettingBoard.SetActive(false);
        finishBoard.SetActive(false);
        settingScreenBoard.SetActive(false);
    }
}
