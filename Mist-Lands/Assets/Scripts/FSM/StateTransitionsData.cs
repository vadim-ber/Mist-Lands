using UnityEngine;

[CreateAssetMenu(fileName = "StateTransitionsData",
    menuName = "ScriptableObjects/FSM/Create StateTransitionsData")]
public class StateTransitionsData : ScriptableObject
{
    [SerializeField] private State[] _list;

    public State[] List
    {
        get => _list;
    }
}
