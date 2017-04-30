﻿using UnityEngine;
using System.Collections;

public class ScorpionVenom : StatusEffect {

    [Header("Parameters")]
    [Range(0, 99)]
    public int damagePerTick;
    [Tooltip("Number of times the poison will apply damage over its duration.")]
    [Range(0, 99)]
    public int damageTicks;
    public Color poisonedTint;

    protected Coroutine poisonCoroutine;

    protected SpriteRenderer sprite;

    public override void StartEffect()
    {
        PoisonHeartTint(poisonedTint);

        poisonCoroutine = StartCoroutine(PoisonTarget());
    }

    private IEnumerator PoisonTarget()
    {
        int tickCount = 0;
        while(tickCount < damageTicks)
        {
            // The first tick of poison damage does not happen immediately
            // upon being poisoned
            yield return new WaitForSeconds(duration / (float)damageTicks);

            // damage
            target.TakeDamage(damagePerTick);

            tickCount++;
        }

        EndEffect();
    }

    protected void PoisonHeartTint(Color tint)
    {
        // Reset heart tint
        if (target is PlayerGridObject)
        {
            PlayerGridObject player = target as PlayerGridObject;

            for (int i = 0; i < UIController.totalHearts; i++)
            {
                player.canvas.healthIcons[i].color = tint;
            }
        }
    }

    public override void EndEffect()
    {
        // Reset the heart tint to default
        PoisonHeartTint(Color.white);

        Destroy(this.gameObject);
    }
}
