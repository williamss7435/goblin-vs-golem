using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillPicker : MonoBehaviour
{
  public const string defaultSkill = "NormalAttack";
  public abstract void Pick(AIPlan plan);
  protected List<Skill> skills{get{return Turn.unit.job.skills;}}
  protected List<Skill> Find(SkillAffectsType type){
        List<Skill> rtv = new List<Skill>();
        foreach(Skill skill in skills){
            if(skill.GetComponentInChildren<SkillAffects>().whoItAffects == type){
                Debug.LogFormat("Skill {0} Selected", skill);
                rtv.Add(skill);
            }
        }
        if(rtv.Count == 0)
            rtv.Add(Default());
        return rtv;
    }

    protected Skill Find(string skillName){
        Skill skill = skills.Find(x=> x.name == skillName);
        if(skill == null)
            return Default();
        return skill;
    }

    protected Skill Default(){
        return skills.Find(x=> x.name == defaultSkill);
    }
}
