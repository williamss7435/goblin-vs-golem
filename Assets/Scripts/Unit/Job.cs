using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Job : ScriptableObject
{
   public List<Stat> stats;
   public List<Skill> skills;
   public Sprite portrait;
   [Multiline]
   public string description;
   public GameObject AISkillPicks;
   
   public void InitStats(){
        stats = new List<Stat>();
        for(int i = 0; i<=(int) StatsEnum.MOV; i++){
            Stat temp = new Stat();
            temp.type = (StatsEnum)i;
            stats.Add(temp);
        }
   }

   public static void LevelUp(Unit unit, int amount){
       Stats toLevelStats = unit.stats;
       foreach(Stat s in toLevelStats.stats){
           s.baseValue+=Mathf.FloorToInt(s.growth*amount);
       }
       toLevelStats[StatsEnum.LVL].baseValue+=amount;
       toLevelStats[StatsEnum.HP].baseValue = toLevelStats[StatsEnum.MaxHP].baseValue;
       toLevelStats[StatsEnum.MP].baseValue = toLevelStats[StatsEnum.MaxMP].baseValue;
       unit.UpdateStats();
   }
}
