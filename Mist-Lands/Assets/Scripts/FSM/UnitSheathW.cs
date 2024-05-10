using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSheathWeapon", menuName = "ScriptableObjects/FSM/Create UnitSheathWeapon")]
public class UnitSheathW : UnitState
{   
    private Weapon _formerWeapon;
    public override void CheckSwitchState(Unit unit)
    {
        if (unit.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
        {           
            SwitchState((UnitState)Transitions.List[8], unit);
        }
    }

    public override void EnterState(Unit unit)
    {
        _formerWeapon = unit.WeaponInHands;
        unit.Selector.WeaponSwapInvoked = false;
        unit.Animator.Play(CurrentStateAnimationName, AnimationLayer);
    }

    public override void ExitState(Unit unit)
    {
        unit.WeaponInHands = unit.EquippiedWeapons.FirstOrDefault(obj => obj != unit.WeaponInHands);
        unit.Animator.runtimeAnimatorController = 
            new AnimatorOverrideController(unit.WeaponInHands.WeaponData.AnimatorController);
        _formerWeapon.Sheath();
        unit.Animator.StopPlayback();
    }

    public override void UpdateState(Unit unit)
    {       
        CheckSwitchState(unit);  
    }
}
