//state establishing methods and variables given to all other states

//no instances created from this class
public abstract class PlayerBaseState
{
    protected PlayerStateMachine ctx;
    protected PlayerStateFactory1 factory;
     public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory1 playerStateFactory){
        ctx = currentContext;
        factory = playerStateFactory;
     }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();

    void UpdateStates(){

    }

    protected void SetSuperState(){

    }

    protected void SetSubState(){

    }

    protected void SwitchState(PlayerBaseState newState){
        //exit from current state
        ExitState();

        //calls the new state into the enter state method
        newState.EnterState();
        ctx.CurrentState = newState;
    }
}
