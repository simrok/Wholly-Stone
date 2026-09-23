using UnityEngine;

// OO가 지닐 수 있는 상태들을 BaseState를 상속받는 각각의 클래스로 구현
// 상태에 따라 수행해야 할 행동을 정의
// 주의할 점: 상태 변경에 대한 로직은 작성하면 안됨
// BaseState를 상속받는 클래스는 다른 BaseState 자식 클래스들에 대해 알 수 없고
// 오직 어떤 행동을 해야 하는지에 대한 내용만을 구현
// 상태 변경에 대한 책임은 OO클래스에게 있다.

public abstract class BaseState
{
    protected Player player;

    public BaseState(Player _player)
    {
        this.player = _player;
    }
    // 상태를 처음 진입했을 때 한 번만 호출되는 메서드
    public abstract void OnStateEnter();
    // 매 프레임마다 호출되어야 하는 메서드
    public abstract void OnStateUpdate();
    public virtual void OnStateFixedUpdate() { }
    // 상태가 변경되면 호출되는 메서드
    public virtual void OnStateExit() { }
}

public class StateMachine
{
    private BaseState curState;
    public BaseState CurState => curState;

    public StateMachine(BaseState _initState)
    {
        ChangeState(_initState);
    }

    public void ChangeState(BaseState _nextState)
    {
        if (curState == _nextState)
            return;

        if (curState != null)
            curState.OnStateExit();

        curState = _nextState;
        curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (curState != null)
            curState.OnStateUpdate();
    }

    public void FixedUpdateState()
    {
        if (curState != null)
            curState.OnStateFixedUpdate();
    }
}