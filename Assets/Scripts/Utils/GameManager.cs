using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string NewScene { get; set; }
    public Data data = new Data();
    public Audio audio = new Audio();
    public Setting setting = new Setting();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        setting.SetupSetting();
        audio.SetupAudio(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        audio.UpdateVolume(setting.SfxVolume, setting.BgmVolume);
    }

    public class Data
    {
        public int InputType { get; set; }
    }

    public class Setting
    {
        public int IsNewPlayer
        {
            get => PlayerPrefs.GetInt("isNewPlayer");
            set => PlayerPrefs.SetInt("isNewPlayer", value);
        }
        
        public float SfxVolume
        {
            get => PlayerPrefs.GetFloat("sfxVolume");
            set => PlayerPrefs.SetFloat("sfxVolume", value);
        }

        public float BgmVolume
        {
            get => PlayerPrefs.GetFloat("bgmVolume");
            set => PlayerPrefs.SetFloat("bgmVolume", value);
        }

        public int Vibration
        {
            get => PlayerPrefs.GetInt("Vibration");
            set => PlayerPrefs.SetInt("Vibration", value);
        }

        public void SetupSetting()
        {
            if (IsNewPlayer == 0)
            {
                IsNewPlayer = 1;
                SfxVolume = 1f;
                BgmVolume = 1f;
                Vibration = 1;
            }
        }
    }

    [Serializable]
    public class Audio
    {
        public Sound[] soundEffects;
        public Sound[] backgroundMusics;

        [HideInInspector] public List<AudioSource> activeSfx = new List<AudioSource>();
        [HideInInspector] public List<AudioSource> activeBgm = new List<AudioSource>();

        public void SetupAudio(GameObject gameObject)
        {
            for (int i = 0; i < soundEffects.Length; i++)
            {
                soundEffects[i].audioSource = gameObject.AddComponent<AudioSource>();
                soundEffects[i].audioSource.clip = soundEffects[i].clip;
                soundEffects[i].audioSource.volume = soundEffects[i].volume;
                soundEffects[i].audioSource.pitch = soundEffects[i].pitch;
                soundEffects[i].audioSource.loop = soundEffects[i].loop;
                activeSfx.Add(soundEffects[i].audioSource);
            }

            for (int i = 0; i < backgroundMusics.Length; i++)
            {
                backgroundMusics[i].audioSource = gameObject.AddComponent<AudioSource>();
                backgroundMusics[i].audioSource.clip = backgroundMusics[i].clip;
                backgroundMusics[i].audioSource.volume = backgroundMusics[i].volume;
                backgroundMusics[i].audioSource.pitch = backgroundMusics[i].pitch;
                backgroundMusics[i].audioSource.loop = backgroundMusics[i].loop;
                activeBgm.Add(backgroundMusics[i].audioSource);
            }
        }

        public void UpdateVolume(float sfxVolume, float bgmVolume)
        {
            for (int i = 0; i < activeSfx.Count; i++)
            {
                activeSfx[i].volume = sfxVolume;
            }

            for (int i = 0; i < activeBgm.Count; i++)
            {
                activeBgm[i].volume = bgmVolume;
            }
        }

        public void PlaySfx(string name)
        {
            Sound sfx = Array.Find(soundEffects, sound => sound.name == name);
            if (sfx == null)
            {
                Debug.LogWarning("Audio " + name + " not found!!");
                return;
            }

            sfx.audioSource.Play();
        }

        public void PlayBgm(string name)
        {
            Sound bgm = Array.Find(backgroundMusics, sound => sound.name == name);
            if (bgm == null)
            {
                Debug.LogWarning("Audio " + name + " not found!!");
                return;
            }

            for (int i = 0; i < backgroundMusics.Length; i++)
            {
                backgroundMusics[i].audioSource.Stop();
            }

            bgm.audioSource.Play();
        }
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
        [Range(0.1f, 3f)] public float pitch;
        public bool loop;
        [HideInInspector] public AudioSource audioSource;
    }
}