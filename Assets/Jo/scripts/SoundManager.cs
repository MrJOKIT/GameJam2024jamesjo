using System;
using UnityEngine;
using UnityEngine.UI;

namespace Jo.scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private Sound[] music,sfx;
        public AudioSource musicSource,sfxSource;
        public Slider musicSlider, sfxSlider;
        public GameObject optionCanvas,optionButton1,optionButton2;

        [Serializable]
        public struct Sound
        {
            public string soundName;
            public AudioClip clip;
            //[Range(0f, 1f)] public float volume;
        }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        
        }

        public void SetupForNewScene()
        {
            optionCanvas.SetActive(false);
            optionButton1.SetActive(true);
            optionButton2.SetActive(false);
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
}