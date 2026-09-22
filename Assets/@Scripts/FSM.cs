using UnityEngine;
public abstract class BaseState
{
    protected Monster monster;
    protected BaseState(Monster _monster)
    {
        monster = _monster;
    }

    // 상태를 처음 진입했을 때 한 번만 호출되는 메서드
    public abstract void OnStateEnter();
    // 매 프레임마다 호출되어야 하는 메서드
    public abstract void OnStateUpdate();
    // 상태가 변경되면 호출되는 메서드
    public abstract void OnStateExit();
}

// Monster가 지닐 수 있는 상태들을 BaseState를 상속받는 각각의 클래스로 구현
// 상태에 따라 수행해야 할 행동을 정의
// 주의할 점: 상태 변경에 대한 로직은 작성하면 안됨
// BaseState를 상속받는 클래스는 다른 BaseState 자식 클래스들에 대해 알 수 없고
// 오직 어떤 행동을 해야 하는지에 대한 내용만을 구현
// 상태 변경에 대한 책임은 Monster클래스에게 있다.
public class IdleState : BaseState
{
    public IdleState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }
}

public class MoveState : BaseState
{
    public MoveState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }
}
public class RollState : BaseState
{
    public RollState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }
}
public class AttackState : BaseState
{
    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
    }
    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }
}

public class FSM
{
    public FSM(BaseState initState)
    {
        _curState = initState;
        ChangeState(_curState);
    }

    private BaseState _curState;

    public void ChangeState(BaseState nextState)
    {
        if (nextState == _curState)
            return;

        if (_curState != null)
            _curState.OnStateExit();

        _curState = nextState;
        _curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (_curState != null)
            _curState.OnStateUpdate();
    }
}