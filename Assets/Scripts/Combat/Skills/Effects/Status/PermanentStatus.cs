using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentStatus : CombatStatus
{
    public override void Effect()
    {
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach(Modifier m in modifiers){
            m.Activate(unit);
        }
    }
}
