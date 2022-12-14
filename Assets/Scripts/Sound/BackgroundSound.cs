using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] AudioClip m_backgroundMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.BackgroundMusicAudioSource.PlayOneShot(m_backgroundMusic);
    }
}
