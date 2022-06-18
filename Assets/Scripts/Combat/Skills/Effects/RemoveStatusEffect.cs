using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : SkillEffect
{
    public List<string> statusName;
    public override void Apply(Unit target)
    {
        foreach(string s in statusName){
            SeekAndDestroy(s, target);
        }
    }

    public override int Predict(Unit target)
    {
        return 0;
    }

    void SeekAndDestroy(string statusName, Unit target){
        Transform holder = target.transform.Find("Status");
        Transform status = holder.Find(statusName);
        if(status!=null) {
            target.PopCombatText("^", Color.blue);
            Destroy(status.gameObject);
        }
    }
}
