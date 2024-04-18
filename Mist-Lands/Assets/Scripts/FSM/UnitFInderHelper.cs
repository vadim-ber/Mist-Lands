public class UnitFInderHelper 
{
    public void FindUnitsInContact(Unit unit)
    {
        var list = unit.Selector.FindUnitsInRadius(unit, unit.AttackRange, false);
        if (list.Count > 0)
        {
            UnitList.InvokeOnUnitsContact(unit, list.ToArray());
        }
    }
}
