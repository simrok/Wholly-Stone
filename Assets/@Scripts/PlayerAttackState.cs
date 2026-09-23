using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class AttackState : BaseState
{
    public AttackState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }

    public override void OnStateFixedUpdate()
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
        ChangeState(playerMovement.Dir != Vector3.zero ? PlayerState.Move : PlayerState.Idle);
    }

    public override void OnStateExit()
    {
    }
}