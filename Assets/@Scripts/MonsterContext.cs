using UnityEngine;

public class MonsterContext
{
    // 컴포넌트 참조
    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    
    public void Init(Rigidbody _rb, Animator _anim)
    {
        Rb = _rb;
        Animator = _anim;
    }

    // 체력
    public float monsterHp;
    public float monsterMaxHp = 100f;

    // 이동
    public float monsterMoveSpeed;  // 이동 속도

    // 공격
    private float attackSpeed;  // 공격 속도
    private float attackDamage; 
}
