using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType{
    Physical,
    Magical
}
public class DamageEffect : SkillEffect
{
    public float baseDamageMultiplier= 1;
    public float randomNess = 0.2f;
    public DamageType damageType;
    public float gotHitDelay = 0.3f;
    public override int Predict(Unit target){
        float attackScore =0;
        float defenderScore =0;

        if(damageType == DamageType.Physical){
            attackScore += Turn.unit.GetStat(StatsEnum.ATK);
            defenderScore += target.GetStat(StatsEnum.DEF);
        }else if(damageType == DamageType.Magical){
            attackScore += Turn.unit.GetStat(StatsEnum.MATK);
            defenderScore += target.GetStat(StatsEnum.MDEF);
        }

        float calculation = (attackScore-(defenderScore/2))*baseDamageMultiplier;
        calculation = Mathf.Clamp(calculation, 0 ,calculation);

        return (int) calculation;
    }

    public override void Apply(Unit target){
        int damage = Predict(target);
        int currentHP = target.GetStat(StatsEnum.HP);
        float roll = UnityEngine.Random.Range(1-randomNess, 1+randomNess);
        int finalDamage = (int)(damage*roll);

        target.SetStat(StatsEnum.HP, -finalDamage);
        
        Turn.unit.animationController.Idle();
        Turn.unit.animationController.Attack();
        
        if(target.GetStat(StatsEnum.HP)<= 0) {
            target.animationController.Death(gotHitDelay); 
        }else {
            target.animationController.Idle();
            target.animationController.Hit(gotHitDelay);
        }
    }
}
