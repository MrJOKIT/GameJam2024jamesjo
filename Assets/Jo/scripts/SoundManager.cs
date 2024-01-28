using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private Sound[] music,sfx;
    public AudioSource musicSource,sfxSource;
    public Slider musicSlider, sfxSlider;

    [Serializable]
    public struct Sound
    {
        public string soundName;
        public AudioClip clip;
        //[Range(0f, 1f)] public float volume;
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }
    

    public void PlayMusic(string musicName)
    {
        Sound s = Array.Find(music, x => x.soundName == musicName);

        musicSource.clip = s.clip;
        //musicSource.volume = s.volume;
        musicSource.Play();
    }

    public void PlaySfx(string sfxName)
    {
        Sound s = Array.Find(sfx, x => x.soundName == sfxName);

        //sfxSource.volume = s.volume;
        sfxSource.PlayOneShot(s.clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ChangeMusicVolume(float slider)
    {
        musicSource.volume = slider;
        PlayerPrefs.SetFloat("MusicVolume",slider);
    }

    public void ChangeSfxVolume(float slider)
    {
        sfxSource.volume = slider;
        PlayerPrefs.SetFloat("SfxVolume",slider);
    }
}