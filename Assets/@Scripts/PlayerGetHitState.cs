using UnityEngine;
using UnityEngine.Playables;

public class PlayerGetHitState
{
    public PlayerGetHitState(Player player) : base(player) { }
    private PlayerContext context = new PlayerContext();

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
        if (context.playerHp <= 0 && state != PlayerState.Dead)
        {
            ChangeState(PlayerState.Dead);
            return;
        }
    }

    public override void OnStateFixedUpdate()
    {
        base.OnStateFixedUpdate();
    }

    public override void OnStateExit()
    {
    }
}
