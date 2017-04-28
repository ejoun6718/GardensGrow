
using UnityEngine;
public abstract class ScorpionAttackAbstractFSM : AttackModule {
     
    protected float transitionedAt;
    public enum State { 
        Ready = 0,
        ClawAttack = 1,
        ChargingTail = 3,
        TailAttack = 4,
        AttackCooldown = 5,
        TailStuck = 6
    }  
    public State state = State.Ready;

    public override void Step()
    {
		State prevState = state;
		 
                switch(state) {
                    case State.Ready:
                        ExecuteActionReady();
                        break;
                    case State.ClawAttack:
                        ExecuteActionClawAttack();
                        break;
                    case State.ChargingTail:
                        ExecuteActionChargingTail();
                        break;
                    case State.TailAttack:
                        ExecuteActionTailAttack();
                        break;
                    case State.AttackCooldown:
                        ExecuteActionAttackCooldown();
                        break;
                    case State.TailStuck:
                        ExecuteActionTailStuck();
                        break;
            }

// The following switch statement handles the HLSM's state transition logic
            switch(state) {
                case State.Ready:
                    if( ShouldTailAttack() ) 
                        state = State.ChargingTail;
                    if( ShouldClawAttack() ) 
                        state = State.ClawAttack;
                    break;
                case State.ClawAttack:
                    state = State.AttackCooldown;
                    break;
                case State.ChargingTail:
                    if( IsTailChargeComplete() ) 
                        state = State.TailAttack;
                    break;
                case State.TailAttack:
                    if( HasTailHit() ) 
                        state = State.AttackCooldown;
                    else 
                       state = State.TailStuck;
                    break;
                case State.AttackCooldown:
                    if( HasFinishedCooldown() ) 
                        state = State.Ready;
                    break;
                case State.TailStuck:
                    if( IsTailUnstuck() ) 
                        state = State.AttackCooldown;
                    break;
            }		
		
            if (prevState != state)
            {
                transitionedAt = Time.time;
                OnTransition();
            }
			
    }
    // State Logic Functions
    protected abstract void ExecuteActionReady();
    protected abstract void ExecuteActionClawAttack();
    protected abstract void ExecuteActionChargingTail();
    protected abstract void ExecuteActionTailAttack();
    protected abstract void ExecuteActionAttackCooldown();
    protected abstract void ExecuteActionTailStuck();
    // Transitional Logic Functions
    protected abstract bool ShouldTailAttack();
    protected abstract bool HasFinishedCooldown();
    protected abstract bool IsTailChargeComplete();
    protected abstract bool IsTailUnstuck();
    protected abstract bool HasTailHit();
    protected abstract bool ShouldClawAttack();
    public float TimeInState()
    {
        return Time.time - transitionedAt;
    }

    
    protected virtual void OnTransition() { }
  
}