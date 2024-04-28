using UnityEngine;

[CreateAssetMenu(fileName = "StateTransitionsData",
    menuName = "ScriptableObjects/FSM/Create StateTransitionsData")]
public class StateTransitionsData : ScriptableObject
{
    [SerializeField] private State[] _transitions;

    public State[] Transitions
    {
        get => _transitions;
    }
}
