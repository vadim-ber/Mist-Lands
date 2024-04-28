
public abstract class TeamState : State
{   
    public abstract void EnterState(Team team);
    public abstract void ExitState(Team team);
    public abstract void UpdateState(Team team);
    public abstract void CheckSwitchState(Team team);

    protected void SwitchState(TeamState newState, Team team)
    {
        ExitState(team);
        newState.EnterState(team);
        team.State = newState;
    }
}
