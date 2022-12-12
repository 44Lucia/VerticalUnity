using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i { 
        get{
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                return _i;
        }
    }

    [SerializeField] private SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip{
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] SoundAudioClipsArray { get => soundAudioClips; set => soundAudioClips = value; }

}
