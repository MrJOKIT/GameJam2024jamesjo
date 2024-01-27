using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private Sound[] music,sfx;
    public AudioSource musicSource,sfxSource;

    [Serializable]
    public struct Sound
    {
        public string soundName;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
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

    public void PlayMusic(string musicName)
    {
        Sound s = Array.Find(music, x => x.soundName == musicName);

        musicSource.clip = s.clip;
        musicSource.volume = s.volume;
        musicSource.Play();
    }

    public void PlaySfx(string sfxName)
    {
        Sound s = Array.Find(sfx, x => x.soundName == sfxName);

        sfxSource.volume = s.volume;
        sfxSource.PlayOneShot(s.clip);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    
}