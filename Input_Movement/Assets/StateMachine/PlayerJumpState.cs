using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory1 playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }

    public override void EnterState(){
        HandleJump();
    }
    public override void UpdateState(){
        CheckSwitchState();
    }
    public override void ExitState(){
        
    }
    public override void CheckSwitchState(){
        
    }
    public override void InitializeSubState(){
        
    }   
    void HandleJump(){       
            ctx.Animator.SetBool("isJumping", true);
            ctx.IsJumping = true;
            //adding jump velocity to the moving and running values
            ctx.currentMovementY = ctx.InitialJumpVelocity * 0.5f;
            ctx.currentMovementY = ctx.InitialJumpVelocity * 0.5f;   

    }
}

