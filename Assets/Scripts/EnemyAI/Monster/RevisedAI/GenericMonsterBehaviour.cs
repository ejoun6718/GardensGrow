﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GenericMonsterBehaviour : MonsterBehaviourAbstractFSM {

    [Header("Behaviour Modules")]
    public PathFindingBehaviour pathFindingModule;
    public BasicAttackModule attackModule;

    [Header("Behaviour Parameters")]
    public bool isDisabled;

    [Header("Parameters for Modules")]
    public PathFindingBehaviour.PathFindingParameters pathFindingParameters;

    protected override void Start()
    {
        pathFindingModule.SetParameters(pathFindingParameters);

        if (isDisabled)
            Disable();

        base.Start();
    }

    public override void Reset()
    {
    }

    public void Disable()
    {
        isDisabled = true;
        state = State.Disabled;
    }

    public void Enable()
    {
        isDisabled = false;
        state = State.PathFinding;
    }

    // ================================================
    // | States
    // ================================================

    protected override IEnumerator ExecuteActionPathFinding()
    {
        pathFindingModule.Step();

        yield return null;
    }

    protected override IEnumerator ExecuteActionDamaged()
    {
        yield return null;
    }

    protected override IEnumerator ExecuteActionDetect()
    {
        yield return null;
    }

    protected override IEnumerator ExecuteActionAttack()
    {
        attackModule.Step();

        yield return null;
    }

    protected override IEnumerator ExecuteActionDisabled()
    {
        yield return null;
    }

    // ================================================
    // | Transitions
    // ================================================

    protected override bool Recovered()
    {
        // TODO:
        return false;
    }

    protected override bool OnHit()
    {
        // TODO:
        return false;
    }

    protected override bool Detected()
    {
        // TODO:
        return false;
    }

    protected override bool CanAttack()
    {
        AttackCollider attackCollider = getHitColliderFromDirection(direction);

        List<KillableGridObject> killList = attackCollider.GetKillList();

        if (killList.Count > 0)
        {
            // Check if the killable is an enemy
            foreach(KillableGridObject tar in killList)
            {
                if (tar.faction != this.faction)
                    return true;
            }

            return false;
        }
        else
            return false;
    }

}
