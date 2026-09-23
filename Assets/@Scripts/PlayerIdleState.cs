using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();
    }

    public override void OnStateExit()
    {
    }
}