using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatLog
{
    
    public static void CheckActive(){
        foreach(Unit unit in StateMachineController.instance.units){
            if(unit.GetStat(StatsEnum.HP)<=0){
                unit.active = false;
            }else{
                unit.active = true;
            }
        }
    }

    public static bool IsOver(){
        int activeAlliances = 0;

        for (int i = 0; i < MapLoader.instance.alliances.Count; i++)
            activeAlliances+= CheckAlliance(MapLoader.instance.alliances[i]);
        
        if(activeAlliances>1) return false;
        
        return true;
    }

    public static int CheckAlliance(Alliance alliance){
        for (int i = 0; i < alliance.units.Count; i++){
            Unit currentUnit = alliance.units[i];
            if(currentUnit.active){
                return 1;
            }
        }
        return 0;
    }
}
