using UnityEngine;

public abstract class UnitState : State
{    
    [SerializeField] private string _currentStateAnimationName;
    [SerializeField] private float _animationTrasitionTime = 0f;
    [SerializeField] private int _animationLayer = 0;
    
    public string CurrentStateAnimationName
    {
        get => _currentStateAnimationName;
    }
    public float AnimationTrasitionTime
    {
        get => _animationTrasitionTime;
    }
    public int AnimationLayer
    {
        get => _animationLayer;
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
    }
}
