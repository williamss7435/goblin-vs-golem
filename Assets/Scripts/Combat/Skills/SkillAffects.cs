using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillAffectsType{
    Default,
    AllyOnly,
    EnemyOnly,
}
public class SkillAffects : MonoBehaviour
{
    public SkillAffectsType whoItAffects;

    public bool IsTarget(Unit unit){
        switch(whoItAffects){
            case SkillAffectsType.AllyOnly:
                return IsAlly(unit);
            case SkillAffectsType.EnemyOnly:
                return IsEnemy(unit);
            default:
                return true;
        }
    }

    public bool IsAlly(Unit unit){
        return unit.alliance == Turn.unit.alliance;
    }

    public bool IsEnemy(Unit unit){
        return unit.alliance != Turn.unit.alliance;
    }
}
