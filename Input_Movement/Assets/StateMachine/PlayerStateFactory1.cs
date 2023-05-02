public class PlayerStateFactory1
{
    PlayerStateMachine context;

    public PlayerStateFactory1(PlayerStateMachine currentContext){
        context = currentContext;
    }

    public PlayerBaseState Idle(){
        return new PlayerIdleState(context, this);
    }

    public PlayerBaseState Walking(){
        return new PlayerWalkingState(context, this);
    }

    public PlayerBaseState Running(){
        return new PlayerRunningState(context, this);
    }

    public PlayerBaseState Jumping(){
        return new PlayerJumpState(context, this);
    }

    public PlayerBaseState Grounded(){
        return new PlyaerGroundedState(context, this);
    }
}
