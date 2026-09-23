using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    private StateMachine stateMachine;
    private Rigidbody rb;
    private Animator animator;
    public PlayerContext context = new PlayerContext();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.7f);
    }

    private void Start()
    {
        context.Init(rb, animator);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        context.playerMovement = GetComponent<PlayerMovement>();
        context.isAttack = false;
        context.isInvincible = false;
        context.playerHp = 100f;
        context.normalAttackDamage = 10f;
    }

    private void Update()
    {
        // 방향키 입력이 있으면 1:Move, 없으면 0:Idle
        animator.SetFloat("Blend", context.playerMovement.Dir != Vector3.zero ? 1f : 0f);

        // DEAD 상태인지 매 프레임 체크

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
        playerHp = maxHp;
        transform.position = respawnPoint;
        ChangeState(PlayerState.Idle);
    }
    // 구르기 콤보


    // 콤보 구현(애니메이션 작업 필요)
    private bool bComboExist;
    private bool bComboEnable;  // 콤보 가능한지
    private int comboIndex;
    private void ResetCombo() { isAttack = false; comboIndex = 0; } // 콤보 리셋 허용, 값 설정 불가

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




