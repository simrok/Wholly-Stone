using UnityEngine;
using UnityEngine.Playables;

public class PlayerGetHitState : PlayerState
{
    public PlayerGetHitState(Player player) : base(player) { }

    // 구현 해야함
    public bool IsDone { get; }

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
