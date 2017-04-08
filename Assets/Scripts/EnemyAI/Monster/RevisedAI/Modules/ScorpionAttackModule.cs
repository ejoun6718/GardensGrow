﻿using UnityEngine;
using System.Collections;
using System;

public class ScorpionAttackModule : ScorpionAttackAbstractFSM {

    public ScorpionAttackParameters parameters;

    // Change this based on animator
    protected string tailAttackTrigger = "TailAttack";
    // Change this based on animator
    protected string clawAttackTrigger = "ClawAttack";
    protected bool tailIsCharging = false;
    protected bool tailIsStuck = false;
    protected bool tailHasHit = false;

    /// <summary>
    /// Depending on the state of the attack, the scorpion
    /// can or cannot move.
    /// 
    /// For example, it cannot move if the tail is stuck but
    /// it can move if the attack is on cooldown.
    /// </summary>
    public bool CanMove()
    {
        return tailIsCharging || tailIsStuck;
    }

    // =====================================================
    // | States
    // =====================================================

    protected override void ExecuteActionReady()
    {
        tailIsCharging = false;
        tailIsStuck = false;
        tailHasHit = false;
    }

    protected override void ExecuteActionClawAttack()
    {
        parameters.creature.Attack(clawAttackTrigger);
    }

    protected override void ExecuteActionChargingTail()
    {
        tailIsCharging = true;
    }

    protected override void ExecuteActionTailAttack()
    {
        parameters.creature.Attack(tailAttackTrigger);

        // Check if the tail hit anything
        tailHasHit = parameters.creature.HitSomething();
    }

    protected override void ExecuteActionAttackCooldown()
    {
        tailIsCharging = false;
        tailIsStuck = false;
    }

    protected override void ExecuteActionTailStuck()
    {
        tailIsStuck = true;
    }

    // =====================================================
    // | States
    // =====================================================

    protected override bool ShouldTailAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override bool HasFinishedCooldown()
    {
        if (TimeInState() > parameters.attackCooldown)
            return true;
        else
            return false;
    }

    protected override bool IsTailChargeComplete()
    {
        if (TimeInState() > parameters.tailChargeDuration)
            return true;
        else
            return false;
    }

    protected override bool IsTailUnstuck()
    {
        if (TimeInState() > parameters.tailStuckDuration)
            return true;
        else
            return false;
    }

    protected override bool HasTailHit()
    {
        return tailHasHit;
    }

    protected override bool ShouldClawAttack()
    {
        throw new System.NotImplementedException();
    }

    [Serializable]
    public class ScorpionAttackParameters : AttackAbstractParameters
    {
        [Range(0.0f, 60.0f)]
        public float attackCooldown;
        [Range(0, 99)]
        public int numClawAttacksBeforeTail = 2;
        [Range(0.0f, 60.0f)]
        public float tailChargeDuration;
        [Range(0.0f, 60.0f)]
        public float tailStuckDuration;
    }
}
