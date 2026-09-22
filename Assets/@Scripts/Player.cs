using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    private Coroutine routine;
    public enum PlayerState { Idle, Move, Roll, Attack, GetHit, Dead }
    private PlayerState state = PlayerState.Idle;

    private Rigidbody rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    private Enemy enemy;

    private float hp { get; set; }
    private float maxHp = 100f;

    // 상태:죽음
    private float respawnDelay;
    private Vector3 respawnPoint;

    private float attackSpeed;
    private float normalAttackDamage;

    // 관통력, .. 등 추가

    private bool isAttack;  // 공격 중
    private bool isSkilled; // 스킬 시전 중

    [Header("구르기 값")]
    [SerializeField] private float rollDuration = 0.4f; // 지속시간 0.35~0.45
    [SerializeField] private float rollMoveSpeed = 60f; // 구르기 이동 속도(거리 = 속력 * 시간)
    private float invincibleRatio = 0.65f;  // 무적상태는 전체 중 앞 65% 구간 지속
    [SerializeField] private AnimationCurve rollSpeedCurve;
    private float recoveryTime = 0.1f;    // 재입력 불가 구간 0.1~0.15초

    private bool gotHit;    //  적에게 피격당하고 있는지
    private bool isInvincible; // 무적 상태인지
    //private bool isRoll;
    // 회피 도중 방향 전환되지 않게 하기 위한 변수
    Vector3 rollVec;
    private float elapsed;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.7f);
    }

    private void Start()
    {
        state = PlayerState.Idle;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        isAttack = false;
        isInvincible = false;
        hp = 100f;
        normalAttackDamage = 10f;
    }

    private void Update()
    {
        // 방향키 입력이 있으면 1:Move, 없으면 0:Idle
        //animator.SetFloat("Blend", playerMovement.Dir != Vector3.zero ? 1f : 0f);

        // DEAD 상태인지 매 프레임 체크
        if (hp <= 0 && state != PlayerState.Dead)
        {
            ChangeState(PlayerState.Dead); return;
        }
        // 적에게 피격 당하고 있을 때
        if (gotHit && state != PlayerState.Roll)
        {
            ChangeState(PlayerState.GetHit);
            return;
        }
        switch (state)
        {
            case PlayerState.Move:
                // Move 행동 구현
                ChangeState(PlayerState.Move);
                break;
            case PlayerState.Roll:
                // Roll 행동 구현
                ChangeState(PlayerState.Roll);
                break;
            case PlayerState.Attack:
                // Attack 행동 구현
                ChangeState(PlayerState.Attack);
                break;
        }

        //D: 기본 공격
        if (Input.GetKeyDown(KeyCode.D) && CanAct()) 
        {
            ChangeState(PlayerState.Attack);
        }
        // S: 구르기
        if (Input.GetKeyDown(KeyCode.S) && CanAct())
        {
            ChangeState(PlayerState.Roll);
        }
        // A: 콤보
        if (Input.GetKeyDown(KeyCode.A) && CanAct())
        {

        }
        // W: 스킬1 사용
        if (Input.GetKeyDown(KeyCode.C) && CanAct())
        { 
        }
        // E: 스킬2 사용
        if (Input.GetKeyDown(KeyCode.E) && CanAct())
        {
        }
        // R: 상호 작용
    }

    public void ChangeState(PlayerState nextState)
    {
        if (nextState == state) return;
        state = nextState;

        playerMovement.canMove = (nextState == PlayerState.Idle || nextState == PlayerState.Move);

        if (nextState == PlayerState.Idle)
        {

        }
        if (nextState == PlayerState.Roll)
        {
            animator.SetTrigger("Roll");
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(RollRoutine());
        }
        if (nextState == PlayerState.Attack)
        {
            animator.SetTrigger("Attack");
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(AttackRoutine());
        }
        if (nextState == PlayerState.GetHit)
        {
            animator.SetTrigger("GetHit");
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(GetHitRoutine());
        }
        if (nextState == PlayerState.Dead)
        {
            animator.SetTrigger("Dead");
            if (routine != null) StopCoroutine(routine);
            routine = StartCoroutine(DeadRoutine());
        }
    }

    public bool CanAct()
    {
        return state == PlayerState.Idle || state == PlayerState.Move;
    }

    private IEnumerator AttackRoutine()
    {
        //공격이 Enemy 한테 맞으면 적의 hp 깎기
        Collider[] colls = Physics.OverlapSphere(transform.position, 0.7f);
        foreach (Collider coll in colls)
        {
            enemy = coll.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(normalAttackDamage);
            }
        }
        yield return null;
        ChangeState(playerMovement.Dir != Vector3.zero ? PlayerState.Move : PlayerState.Idle);
    }

    // 구르기 코루틴
    private IEnumerator RollRoutine()
    {
        elapsed = 0f;
        rollVec = transform.forward;
        Vector3 startPos = transform.position;

        while (elapsed <= rollDuration)
        {
            // 무적 프레임은 앞쪽 60~70%. 
            isInvincible = elapsed < rollDuration * invincibleRatio;
            // 현재 바라보고 있는 방향으로 전진하면서 구르는 애니메이션
            // 지속시간 0.35~0.45초.  이동거리는 캐릭터 크기의 2~3배 정도
            float speed = rollMoveSpeed * rollSpeedCurve.Evaluate(elapsed / rollDuration);
            Debug.Log($"테스트: speed={speed}, rollMoveSpeed={rollMoveSpeed}");
            playerMovement.ForceMove(rollVec, speed);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isInvincible = false;

        // 구르기 종료 후 아주 짧은 재입력 불가 구간 0.1~0.15초
        yield return new WaitForSeconds(recoveryTime);

        // 구르기 사용시 진행 중이던 콤보 카운터는 리셋
        isAttack = false;

        // 방향키 입력이 있으면 Move, 없으면 Idle
        ChangeState(playerMovement.Dir != Vector3.zero ? PlayerState.Move : PlayerState.Idle);
    }
    private IEnumerator GetHitRoutine()
    {
        gotHit = true;
        // 리스폰 시간
        yield return new WaitForSeconds(respawnDelay);
        
        ChangeState(playerMovement.Dir != Vector3.zero ? PlayerState.Move : PlayerState.Idle);
    }

    private IEnumerator DeadRoutine()
    {
        // 부활
        yield return new WaitForSeconds(respawnDelay);
        hp = maxHp;
        transform.position = respawnPoint;
        ChangeState(PlayerState.Idle);
    }
    // 구르기 콤보


    // 콤보 구현(애니메이션 작업 필요)
    private bool bComboExist;
    private bool bComboEnable;  // 콤보 가능한지
    private int comboIndex;

    private void Combo_Enable()
    {
        bComboEnable = true;
    }

    private void Combo_Disable()
    {
        bComboEnable = false;
    }

    private void Combo_Exist()
    {
        if (bComboExist == false) return;
        bComboExist = true;
    }
}




