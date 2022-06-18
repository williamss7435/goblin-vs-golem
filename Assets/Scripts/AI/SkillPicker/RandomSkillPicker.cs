using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkillPicker : SkillPicker
{
    public List<string> skillsToPick;
    public override void Pick(AIPlan plan){
        if(skillsToPick!=null && skillsToPick.Count!=0){
            plan.skill = Find(skillsToPick[Random.Range(0,skillsToPick.Count)]);
        }else{
            plan.skill = skills[Random.Range(0, skills.Count)];
        }
        plan.targetType = plan.skill.GetComponentInChildren<SkillAffects>().whoItAffects;
    }
}
