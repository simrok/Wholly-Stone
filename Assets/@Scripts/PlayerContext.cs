using UnityEngine;

// 플레이어 컨텍스트
[System.Serializable]
public class PlayerContext
{
    // 컴포넌트 참조
    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }

    public void Init(Rigidbody _rb, Animator _anim, PlayerMovement _playerMovement)
    {
        Rb = _rb;
        Animator = _anim;
        PlayerMovement = _playerMovement;
    }

    // 체력
    public float playerHp;
    public float playerMaxHp = 100f;

    // Attack
    public float attackDamage;
    //public float attackSpeed;   // 공격 속도
    public float attackDuration = 0.44f;    // 공격 시간
    public float attackRecoveryTime = 0.2f; // 재입력 불가 구간 

    // Roll
    public float rollDuration = 0.67f; // 지속시간 0.35~0.45
    public float rollMoveSpeed = 20f; // 구르기 이동 속도(거리 = 속력 * 시간)
    public float invincibleRatio = 0.65f;  // 무적상태는 전체 중 앞 65% 구간 지속
    public AnimationCurve rollSpeedCurve;
    public float rollRecoveryTime = 0.1f;    // 재입력 불가 구간 0.1~0.15초
    public bool isInvincible; // 무적 상태인지

    // GetHit
    public bool gotHit;    //  적에게 피격당하고 있는지

    // Dead
    public float respawnDelay;
    public Vector3 respawnPoint;


    // 구르기 콤보
    private bool bComboExist;
    private bool bComboEnable;  // 콤보 가능한지
    private int comboIndex;
}
