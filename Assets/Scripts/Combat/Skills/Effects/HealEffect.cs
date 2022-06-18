using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : SkillEffect
{
    public float baseDamageMultiplier = 1;
    public float randomNess = 0.3f;

    public override int Predict(Unit target)
    {
        return (int)(Turn.unit.GetStat(StatsEnum.MATK)*baseDamageMultiplier);
    }

    public override void Apply(Unit target)
    {
        int heal = Predict(target);

        int currentHP = target.GetStat(StatsEnum.HP);
        float roll = UnityEngine.Random.Range(1-randomNess, 1+randomNess);
        int finalHeal = (int)(heal*roll);
        
        target.SetStat(
            StatsEnum.HP, 
            finalHeal
        );
        
    }
}
