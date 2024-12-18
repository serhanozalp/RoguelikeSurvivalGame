public class PlayerIdleState : BaseState<PlayerLocomotionContextData>
{
    public PlayerIdleState(BaseState<PlayerLocomotionContextData> parentState, PlayerLocomotionContextData contextData) : base(parentState, contextData)
    {
    }
}
