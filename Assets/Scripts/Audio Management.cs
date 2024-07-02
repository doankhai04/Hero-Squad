using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement instance;
    [SerializeField] AudioSource musicSource;
    [Header("Kill Audio")]
    [SerializeField] AudioClip heroDeath;
    [SerializeField] AudioClip monsterDeath;

    [Header("Attack Audio")]
    [SerializeField] AudioClip attackAudio;

    [Header("Click Audio")]
    [SerializeField] AudioClip clickAudio;

    [Header("Setting Values")]
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSLider;
    public float musicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    float defaultVolume = 0.5f;
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
        musicVolumeSlider.maxValue = SFXVolumeSLider.maxValue = 1f;
        musicVolumeSlider.minValue = SFXVolumeSLider.minValue = 0f;
        musicVolumeSlider.value = SFXVolumeSLider.value = defaultVolume;
    }
    private void Update()
    {
        GetSFXVolume();
        GetMusicVolume();
        ChangeMusicVolume();
    }

    public void GetSFXVolume()
    {
        SFXVolume = SFXVolumeSLider.value;
    }
    public void GetMusicVolume()
    {
        musicVolume = musicVolumeSlider.value;
    }
    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolume;
    }
    public void PlayHeroDeathAudio()
    {
        PlayAudio(heroDeath, SFXVolume);
    }
    public void PlayMonsterDeathAudio()
    {
        PlayAudio(monsterDeath, SFXVolume);
    }
    public void PlayAttackAudio()
    {
        PlayAudio(attackAudio, SFXVolume);
    }
    public void PlayClickAudio()
    {
        PlayAudio(clickAudio, SFXVolume);
    }
    void PlayAudio(AudioClip audio, float volume)
    {
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, volume);
    }
   public  void ResetAudio()
    {
        Destroy(gameObject);
    }
}
