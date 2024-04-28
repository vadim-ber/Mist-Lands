using UnityEngine;

[CreateAssetMenu(fileName = "TeamSelected", menuName = "ScriptableObjects/FSM/Create Selected team state")]
public class TeamSelected : TeamState
{
    public override void CheckSwitchState(Team team)
    {
        if(team.Turner.CurrentTeam != team)
        {
            SwitchState((TeamState)Transitions.Transitions[0], team);
        }
    }

    public override void EnterState(Team team)
    {
       
    }

    public override void ExitState(Team team)
    {
        
    }

    public override void UpdateState(Team team)
    {
       CheckSwitchState(team);
    }
}
