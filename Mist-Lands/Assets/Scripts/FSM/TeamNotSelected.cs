
using UnityEngine;

[CreateAssetMenu(fileName = "TeamNotSelected", menuName = "ScriptableObjects/FSM/Create Not Selected team state")]
public class TeamNotSelected : TeamState
{
    public override void CheckSwitchState(Team team)
    {
        if (team.Turner.CurrentTeam == team)
        {
            SwitchState((TeamState)Transitions.Transitions[1], team);
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
