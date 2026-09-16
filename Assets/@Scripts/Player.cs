using UnityEngine;
[RequireComponent(typeof(Rigidbody))] 
public class Player : MonoBehaviour
{
    private Enemy enemy;

    private float hp { get; set; }
    
    private float attackSpeed { get; set; }
    private float normalAttackDamage { get; set; }

    // 관통력, .. 등 추가

    private bool isAttack;  // 공격 중
    private void Awake()
    {
        isAttack = false;
        hp = 100f;
        normalAttackDamage = 10f;
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
            Debug.Log(coll.name);
            enemy = coll.GetComponentInParent<Enemy>();
            if (enemy == null) Debug.Log("enemy가 비었다");
            if (enemy != null)
            {
                Debug.Log("테스트다옹");
                Debug.Log(enemy.hp);
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
    public void playerRoll()
    {
        // 현재 바라보고 있는 방향으로 전진하면서 구르는 애니메이션

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
        // D: 기본 공격
        if (Input.GetKeyDown(KeyCode.D) && !isAttack)
        {
            isAttack = true;
            NormalAttack();

            isAttack = false;
        }
        // S: 구르기
        if (Input.GetKeyDown(KeyCode.S) && !isAttack)
        {
            isAttack = true;
            
        }

        // A: 콤보
        if(Input.GetKeyDown(KeyCode.A) && !isAttack)
        {
            isAttack = true;

        }

        // W: 스킬1 사용
        if(Input.GetKeyDown(KeyCode.C) && !isAttack)
        {
            isAttack = true;

        }

        // E: 스킬2 사용

        // R: 상호 작용
    }

}
