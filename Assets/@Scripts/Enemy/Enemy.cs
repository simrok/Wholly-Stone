using UnityEngine;

public class Enemy: MonoBehaviour
{
    public float hp { get; private set; }

    private float damage { get; set; }
    private float speed { get; set; }
    private float attackSpeed { get; set; }
    private bool isDeath;

    private Rigidbody rb { get; set; }
    
    [SerializeField] private Collider[] colls;
    Collider[] playerInsideZone;
    Collider[] playerOutsideZone;

    private void Awake()
    {
        hp = 100f;
    }

    public bool CheckDeath()
    {
        if (hp <= 0)
        {
            return isDeath = true;
        }
        else
        {
            return isDeath = false;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void GetDamage(float attackDamage)
    {
        // 플레이어의 공격에 따른 데미지 
        // 1. 일반 공격: 휘두르기
        hp -= attackDamage;
        Debug.Log("Player에게 공격을 받았습니다. 현재 Hp: " + hp);
        if (CheckDeath())
        {
            hp = 0;
            Death();
        }
    }

    // Player에게 공격을 함

    // Player가 가까이 오면 근처에 접근하면서 공격을 함
    public void NearAttack(Vector3 pos)
    {

    }

    // 주변 동료 몬스터가 공격당했을 때 같이 '플레이어 공격' 태세로 전환
    public void NearEnemyAttack(Vector3 pos)
    {
        // 반지름 2의 구 안에 콜라이더 붙은 오브젝트들 추출해서 배열에 저장
       Collider[] colls = Physics.OverlapSphere(transform.position, 2f);

       foreach (Collider coll in colls)
       {
            Player player = coll.GetComponent<Player>();
       }
    }


    private void FixedUpdate()
    {
        //playerInsideZone = Physics.OverlapSphere(this.transform.position, 10f);

        //EnableHide(playerInsideZone, false);

        //playerOutsideZone = colls.Except(playerInsideZone).ToArray();

        //EnableHide(playerOutsideZone, true);
    }

    private void EnableHide(Collider player, bool enable)
    {
        //player.GetComponent<MeshRenderer>().enabled = enable;
    }
}
