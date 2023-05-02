using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerGroundedState : PlayerBaseState
{
    public PlyaerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory1 playerStateFactory) : base(currentContext, playerStateFactory)
    {
        
    }

     public override void EnterState(){
        Debug.Log("grounded");
    }
    public override void UpdateState(){
        CheckSwitchState();
    }
    public override void ExitState(){
        
    }
    public override void CheckSwitchState(){
        if (ctx.IsjumpPressed)
        {
            SwitchState(factory.Jumping());
        }
    }
    public override void InitializeSubState(){
        
    }   
}

