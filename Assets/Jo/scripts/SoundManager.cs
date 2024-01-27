using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;

    [Serializable]
    public struct Sound
    {
        public SoundName soundName;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
        public bool loop;
    }

    public void Play(SoundName soundName)
    {
        Sound sound = GetSound(soundName);
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.loop = sound.loop;
        audioSource.Play();
    }

    private Sound GetSound(SoundName soundNamePlay)
    {
        return Array.Find(sounds, s => s.soundName == soundNamePlay);
    }

    public enum SoundName
    {
        BG,
        slip,
        cannonBall,
        hurt,
        lol,
        gameOver,
        explode
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Play(SoundName.BG);
    }
}