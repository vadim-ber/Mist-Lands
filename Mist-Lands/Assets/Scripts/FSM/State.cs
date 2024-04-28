using UnityEngine;

public abstract class State : ScriptableObject
{
    [SerializeField] private StateTransitionsData _transitions;
    
    public StateTransitionsData Transitions
    {
        get => _transitions;
    }
}
