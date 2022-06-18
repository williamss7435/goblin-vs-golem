using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryStatus : CombatStatus
{

    protected override void OnDisable(){
        base.OnDisable();
        unit.onTurnBegin -= DurationCounter;
    }
   public override void Effect()
    {
        unit.onTurnBegin+=DurationCounter;
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach(Modifier m in modifiers){
            m.Activate(unit);
        }
    }

    private void DurationCounter(){
        duration--;
        if(duration<=0)
            Destroy(this.gameObject);
    }
}
