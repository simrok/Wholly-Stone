using UnityEngine;

// 플레이어 컨텍스트
public class PlayerContext
{
    // 컴포넌트 참조
    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerMovement playerMovement;

    public void Init(Rigidbody _rb, Animator _anim)
    {
        Rb = _rb;
        Animator = _anim;
    }

    // 체력
    public float playerHp;
    public float maxHp = 100f;

    // 공격
    public float normalAttackDamage;
    public bool isAttack;  // 공격 중

    // 구르기
    public float rollDuration = 0.67f; // 지속시간 0.35~0.45
    public float rollMoveSpeed = 10f; // 구르기 이동 속도(거리 = 속력 * 시간)
    public float invincibleRatio = 0.65f;  // 무적상태는 전체 중 앞 65% 구간 지속
    public AnimationCurve rollSpeedCurve;
    public float recoveryTime = 0.1f;    // 재입력 불가 구간 0.1~0.15초
    public bool isInvincible; // 무적 상태인지

    // GetHit
    public bool gotHit;    //  적에게 피격당하고 있는지

    // 상태:죽음
    public float respawnDelay;
    public Vector3 respawnPoint;
}
