using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralButtonSoundEffect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] AudioClip m_onHoverAudioClip;
    [SerializeField] AudioClip m_onClickAudioClip;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_onClickAudioClip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_onHoverAudioClip);
    }
}
