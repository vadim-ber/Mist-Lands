using UnityEngine;

public abstract class UnitState : State
{
    [SerializeField] private UnitState[] _transitions;
    [SerializeField] private string _currentStateAnimationName;
    [SerializeField] private float _animationTrasitionTime = 0f;
    public UnitState[] Transitions
    {
        get => _transitions;
    }
    public string CurrentStateAnimationName
    {
        get => _currentStateAnimationName;
    }
    public float AnimationTrasitionTime
    {
        get => _animationTrasitionTime;
    }
    public abstract void EnterState(Unit unit);
    public abstract void ExitState(Unit unit);
    public abstract void UpdateState(Unit unit);
    public abstract void CheckSwitchState(Unit unit);

    protected void SwitchState(UnitState newState, Unit unit)
    {
        ExitState(unit);
        newState.EnterState(unit);
        unit.State = newState;
        Debug.Log(newState.GetType());
    }
}
