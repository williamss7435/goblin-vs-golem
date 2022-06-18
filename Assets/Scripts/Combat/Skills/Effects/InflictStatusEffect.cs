using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusEffect : SkillEffect
{
    
    public CombatStatus status;
    public string statusName;
    public int statusValue;
    public int duration;
    public override int Predict(Unit target){
        return 0;
    }

    public override void Apply(Unit target){
        Transform holder = target.transform.Find("Status");
        Transform stack = holder.Find(statusName);

        if(stack != null) Stack(stack);
        else CreateNew(holder, target);
    }

    void Stack(Transform stack){
        CombatStatus stacktStatus = stack.GetComponent<CombatStatus>();
        stacktStatus.Stack(duration, statusValue);
    }

    void CreateNew (Transform holder, Unit target){
        CombatStatus instantiateStatus = Instantiate(status, holder.position, Quaternion.identity, holder);
        instantiateStatus.name = statusName;
        instantiateStatus.SetModifiers(statusValue);
        instantiateStatus.unit = target;
        instantiateStatus.duration = duration;
        instantiateStatus.Effect();
    }
}
