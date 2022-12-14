using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioMixer m_audioMixer;
    AudioSource m_UIAudioSource;
    AudioSource m_backgroundMusicAudioSource;
    AudioSource m_generalAudioSource;

    float m_maxValue = 0;
    float m_minValue = -80;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);

        m_UIAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        m_backgroundMusicAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
        m_generalAudioSource = transform.GetChild(2).GetComponent<AudioSource>();
    }

    public void PlayAudioClipEffect(AudioClip p_audioClip)
    {
        m_generalAudioSource.PlayOneShot(p_audioClip);
    }

    public void SetMasterVolumeTo(float p_value)
    {
        float value = m_minValue + (m_maxValue - m_minValue) * p_value;
        m_audioMixer.SetFloat("MasterVolume", value);
    }

    public void PlayBackgroundMusic(AudioClip p_audioClip)
    {
        m_backgroundMusicAudioSource.clip = p_audioClip;
        m_backgroundMusicAudioSource.Play();
    }

    public void SetEffectsVolumeTo(float p_value)
    {
        float value = m_minValue + (m_maxValue - m_minValue) * p_value;
        m_audioMixer.SetFloat("EffectsVolume", value);
    }

    public void SetMusicVolumeTo(float p_value)
    {
        float value = m_minValue + (m_maxValue - m_minValue) * p_value;
        m_audioMixer.SetFloat("MusicVolume", value);
    }

    public AudioSource UIEffectsAudioSource { get { return m_UIAudioSource; } }
    public AudioSource BackgroundMusicAudioSource { get { return m_backgroundMusicAudioSource; } }

}
