using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TurnEndState : State
{
    public override void Enter()
    {
        base.Enter();
        CombatLog.CheckActive();
        if(CombatLog.IsOver()){
            if(CombatLog.CheckAlliance(MapLoader.instance.alliances[0]) > 0){
                LoadPanel.instance.GameWin();
            }else {
                LoadPanel.instance.GameLose();
            }
            return;
        }
        StartCoroutine(AddUnitDelay());
    }

    IEnumerator AddUnitDelay(){
        Turn.unit.chargeTime+=300;

        if(Turn.hasMoved)
            Turn.unit.chargeTime+=100;
        
        if(Turn.hasActed)
            Turn.unit.chargeTime+=100;

        Turn.unit.chargeTime-=Turn.unit.GetStat(StatsEnum.SPEED);
        Turn.hasActed = Turn.hasMoved = false;
        Turn.skill = null;

        ComputerPlayer.instance.currentPlan = null;
        
        machineState.units.Remove(Turn.unit);
        machineState.units.Add(Turn.unit);
        yield return new WaitForSeconds(0.5f);

        machineState.ChangeTo<TurnBeginState>();
    }
}
