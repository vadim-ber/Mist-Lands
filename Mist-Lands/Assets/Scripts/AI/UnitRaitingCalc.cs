public static class UnitRaitingCalc
{
   private static readonly float _rangedBunus = -5f;
   public static float Get(Unit unit)
   {
        float w = unit.WeaponInHands.WeaponData.Combat is WeaponData.CombatMode.Ranged ? _rangedBunus : 0f;
        float result = unit.Health.CurrentHealthValue / 10 + unit.ArmorData.Value + w;
        return result;
   }
}
