using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected Enemy enemy;

    protected BaseState(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
