﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FireMonsterBehaviour : MonsterBehaviourAbstractFSM {

    [Header("Behaviour Modules")]
    public PathFindingBehaviour pathFindingModule;
    public BehaviourModule attackModule;

    [Header("AI Parameters")]
    public GameObject mainTarget;

    private GameObject currentTarget;

    protected override void Start()
    {
        if (isDisabled)
            Disable();
        else
            pathFindingModule.SetParameters(pathFindingParameters);

        base.Start();
    }

    public override void Reset()
    {
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

    protected override IEnumerator ExecuteActionAttack()
    {
        attackModule.Step();

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

    protected override IEnumerator ExecuteActionPrimaryBehaviour()
    {
        // TODO: 
        yield return null;
    }

    protected override bool CanMove()
    {
        return true;
    }

    protected override bool CanAct()
    {
        // TODO: 
        return false;
    }
}
