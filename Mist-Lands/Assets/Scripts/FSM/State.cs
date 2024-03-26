using UnityEngine;

public abstract class State : ScriptableObject
{
    [SerializeField] private State[] _transitions;
    [SerializeField] private string _currentStateAnimationName;
    [SerializeField] private float _animationTrasitionTime = 0f;
    public State[] Transitions
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

    protected void SwitchState(State newState, Unit unit)
    {
        ExitState(unit);
        newState.EnterState(unit);
        unit.State = newState;
        Debug.Log(newState.GetType());
    }
}
