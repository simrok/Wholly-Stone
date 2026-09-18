using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))] 
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    private Enemy enemy;

    private float hp { get; set; }

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
    private bool isInvincible; // 무적 상태인지
    private bool isRoll;
    // 회피 도중 방향 전환되지 않게 하기 위한 변수
    Vector3 rollVec;
    private float elapsed;

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

    private void FixedUpdate()
    {
 
    }

    // 일반 공격
    public void NormalAttack()
    {
        // 적에게 기본 공격을 함
        // 팔 휘두르기
        SwingArm();
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
    }
    private void SwingArm()
    {
        Debug.Log("팔 휘두르기 애니메이션 중");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.7f);
    }

    // 앞으로 구르기
    public void Roll()
    {
        isRoll = true;
        playerMovement.canMove = false;
        StartCoroutine(RollRoutine());
    }

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
        Debug.Log("구르는 중");
        Debug.Log(Vector3.Distance(startPos, transform.position));

        isInvincible = false;
        isRoll = false;
        playerMovement.canMove = true;

        // 구르기 종료 후 아주 짧은 재입력 불가 구간 0.1~0.15초
        yield return new WaitForSeconds(recoveryTime);

        // 구르기 사용시 진행 중이던 콤보 카운터는 리셋
        isAttack = false;
    }
    // 구르기 콤보

    // 스킬사용

    
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

    private void Update()
    {
        animator.SetFloat("Blend", playerMovement.Dir != Vector3.zero ? 1f : 0f);
        // D: 기본 공격
        if (Input.GetKeyDown(KeyCode.D) && !isAttack)   // 공격 중이 아니면 공격 가능
        {
            isAttack = true;
            NormalAttack();

            isAttack = false;
            Debug.Log("기본 공격 후 isAttack 상태: " + isAttack);
        }
        // S: 구르기
        if (Input.GetKeyDown(KeyCode.S) && !isAttack && !isRoll)
        {
            isAttack = true;
            animator.SetTrigger("Roll");
            Roll();

            Debug.Log("구르기 후 isAttack 상태: " + isAttack);
        }

        // A: 콤보
        if (Input.GetKeyDown(KeyCode.A) && !isAttack)
        {
            isAttack = true;

        }

        // W: 스킬1 사용
        if (Input.GetKeyDown(KeyCode.C) && !isAttack)
        {
            isAttack = true;
        }

        // E: 스킬2 사용
        if (Input.GetKeyDown(KeyCode.E))
        {
        }

        // R: 상호 작용
    }

}




