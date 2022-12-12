using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/Slow", order = 1)]
public class SlowDownEffect : Effect
{
    [SerializeField] private float slowDownValue;
    public override void handleEffect()
    {
        PlayerController.Instance.Speed -= slowDownValue;
    }

    public float GetSlowDownValue() => slowDownValue;
}
