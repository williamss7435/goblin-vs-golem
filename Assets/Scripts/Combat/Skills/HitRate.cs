using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HitRateType{
    Attack,
    InflicStatus,
    Full
}
public class HitRate : MonoBehaviour
{
   public HitRateType hitRateType;
   public float bonusChance;

    public int Predict(Unit target){
        float hitScore = Turn.unit.GetStat(StatsEnum.ACC);
       float missScore = 0;

        if(hitRateType == HitRateType.Full) return 100;

       if(hitRateType == HitRateType.Attack) missScore = target.GetStat(StatsEnum.EVD);
       else if(hitRateType == HitRateType.InflicStatus) missScore = target.GetStat(StatsEnum.RES);

        float chance = 75-(missScore-hitScore);
        return (int) chance;
    }
   public bool TryHit(Unit target){
        float chance = Predict(target);
        float roll = UnityEngine.Random.Range(0, 100-bonusChance);
        
        Debug.LogFormat(
            "Base chance from stats is {0}. Rolled for {1}+{2} from bonusChance",
            chance, roll, bonusChance
        );

        chance += roll+bonusChance;
        
        return chance>=100;
   }
}
