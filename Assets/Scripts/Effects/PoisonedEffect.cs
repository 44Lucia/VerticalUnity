using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effects/Poison", order = 1)]
public class PoisonedEffect : Effect
{
    [SerializeField, Tooltip("Time in seconds")] private float timerTime;
    [SerializeField] private GameObject impactEffect;

    public override void handleEffect()
    {
        UIManager.Instance.GetTimerUI.gameObject.SetActive(true);

        Job job = new Job(true);
        Task task = new Task(Counter, false);

        job.AddTask(task);

        task = new Task(HitPlayer, false);
        job.AddTask(task);

        job.AddListenerToEndOfJobEvent(() => { CoroutineManager.Instance.RemoveJob(job); });

        job.Start();
    }

    public IEnumerator Counter() 
    {
        float time = timerTime;
        while(time > 0){
            time -= Time.deltaTime;
            UIManager.Instance.GetTimerUI.setTimerTime(time);

            yield return null;
        }
    }

    public IEnumerator HitPlayer() 
    {
        ParticleSystem effectIns = (Instantiate(impactEffect, PlayerController.Instance.transform.position, PlayerController.Instance.transform.rotation)).GetComponent<ParticleSystem>();
        effectIns.gameObject.transform.SetParent(PlayerController.Instance.transform);

        while (GameManager._GAME_MANAGER.GetInCombat) {
            effectIns.Stop();
            effectIns.Play();
            GameManager._GAME_MANAGER.DamagePlayer(1);
            yield return new WaitForSeconds(4f);
        }
        UIManager.Instance.GetTimerUI.gameObject.SetActive(false);
    }
}