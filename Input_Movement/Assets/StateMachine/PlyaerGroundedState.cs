using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerGroundedState : PlayerBaseState
{
    public PlyaerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory1 playerStateFactory){
        
    }

     public override void EnterState(){
        Debug.Log("grounded");
    }
    public override void UpdateState(){
        
    }
    public override void ExitState(){
        
    }
    public override void CheckSwitchState(){
        if (ctx.IsjumpPressed)
        {
            SwitchState(factory.Jumpm());
        }
    }
    public override void InitializeSubState(){
        
    }   
}

