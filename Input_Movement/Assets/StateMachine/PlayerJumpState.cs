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
        
    }
    public override void ExitState(){
        
    }
    public override void CheckSwitchState(){
        
    }
    public override void InitializeSubState(){
        
    }   
    void HandleJump(){
        if ((!isJumping && characterController.isGrounded && isJumpPressed))
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
            //adding jump velocity to the moving and running values
            currentMovement.y = initialJumpVelocity * 0.5f;
            currentRunMovement.y = initialJumpVelocity * 0.5f;   
        }
    }
}

