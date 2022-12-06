using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ANIMATIONS { Idle, Move, Attack, Die, Win, Ultimate}

public class AnimatorController : MonoBehaviour
{
    private Animator mAnimator;

    Dictionary<ANIMATIONS, int> keyValue = new Dictionary<ANIMATIONS, int>();

    private ANIMATIONS currentAnimation;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
        animations();
    }

    private void animations()
    {
        keyValue.Add(ANIMATIONS.Idle, Animator.StringToHash("idle"));
        keyValue.Add(ANIMATIONS.Move, Animator.StringToHash("walk"));
        keyValue.Add(ANIMATIONS.Attack, Animator.StringToHash("attack01"));
        keyValue.Add(ANIMATIONS.Die, Animator.StringToHash("die"));
        keyValue.Add(ANIMATIONS.Win, Animator.StringToHash("victory"));
    }

    public void playAnimation(ANIMATIONS nameAnimation, float transitionTime)
    {
        mAnimator.CrossFade(keyValue[nameAnimation], transitionTime);
        currentAnimation = nameAnimation;
    }

    public bool isAnimationRunning(ANIMATIONS animation) => animation == currentAnimation;
}
