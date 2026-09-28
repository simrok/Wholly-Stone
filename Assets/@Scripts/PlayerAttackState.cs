using UnityEngine;
using System.Collections.Generic;
public class PlayerAttackState : PlayerState
{
    private float elapsed;
    private Vector3 attackDir;

    // 이번 공격에서 이미 맞은 적 목록
    private HashSet<Monster> hitTargets = new HashSet<Monster>();
    

    public PlayerAttackState(Player player) : base(player) { }

    // 구현 해야함
    public bool IsDone => elapsed >= context.attackDuration + context.attackRecoveryTime;

    public override void OnStateEnter()
    {
        context.attackDamage = 5f;
        elapsed = 0f;
        attackDir = owner.transform.forward;
        context.PlayerMovement.canMove = false;

        hitTargets.Clear(); // 이번 공격에서 맞은 적 목록 비우기

        // 현재 바라보고 있는 방향으로 전진하면서 공격하는 애니메이션
        context.Animator.SetTrigger("Attack");
    }
    public override void OnStateUpdate()
    {
    }

    public override void OnStateFixedUpdate()
    {
        //공격이 Enemy 한테 맞으면 적의 hp 깎기
        Collider[] colls = Physics.OverlapSphere(owner.transform.position, 0.7f);
        foreach (Collider coll in colls)
        {
            Monster monster = coll.GetComponentInParent<Monster>();
            if (monster == null) continue;
            if (hitTargets.Add(monster))    // 이번 공격에서 처음 맞는 적일 때만 true
            {
                monster.GetDamage(context.attackDamage);
            }
        }
        elapsed += Time.fixedDeltaTime;
    }

    public override void OnStateExit()
    {
        context.PlayerMovement.canMove = true;  // 움직일 수 있음
    }
}