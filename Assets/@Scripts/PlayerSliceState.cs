using UnityEngine;

public class PlayerSliceState : PlayerState
{
    public PlayerSliceState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateFixedUpdate()
    {
        // 현재 바라보고 있는 방향으로 전진하면서 공격하는 애니메이션
        context.Animator.SetTrigger("Slice");
    }

    public override void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        
    }
}
