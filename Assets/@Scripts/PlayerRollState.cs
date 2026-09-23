using UnityEngine;

public class PlayerRollState : BaseState
{
    public PlayerRollState(Player player) : base(player) { }
    public PlayerContext context = new PlayerContext();

    private float elapsed;
    private Vector3 rollVec;

    public override void OnStateEnter()
    {
        elapsed = 0f;
        rollVec = player.transform.forward;
        Vector3 startPos = player.transform.position;

        // 현재 바라보고 있는 방향으로 전진하면서 구르는 애니메이션
        context.Animator.SetTrigger("Roll");
    }

    public override void OnStateFixedUpdate()
    {
       if (elapsed < context.rollDuration)
        {
            // 무적 프레임은 앞쪽 60~70%. 
            context.isInvincible = elapsed < context.rollDuration * context.invincibleRatio;

            // 지속시간 0.35~0.45초.
            float speed = context.rollMoveSpeed * context.rollSpeedCurve.Evaluate(elapsed / context.rollDuration);
            // Debug.Log($"테스트: speed={speed}, rollMoveSpeed={player.RollMoveSpeed}");
            context.playerMovement.ForceMove(rollVec, speed);
        }
       else
        {
            // 구르기 종료 후 아주 짧은 재입력 불가 구간 0.1~0.15초
            //yield return new WaitForSeconds(recoveryTime);
        }

        elapsed += Time.fixedDeltaTime;
        // 구르기 사용시 진행 중이던 콤보 카운터는 리셋

    }
    public override void OnStateUpdate()
    {
        context.isInvincible = false;
        context.playerMovement.canMove = false;
    }
}
