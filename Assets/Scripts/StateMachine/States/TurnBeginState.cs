using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBeginState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(SelectUnit());
    }

    IEnumerator SelectUnit(){
        BreakDraw();
        machineState.units.Sort((x, y)=>x.chargeTime.CompareTo(y.chargeTime));
        Turn.unit = machineState.units[0];
        
        yield return null;
        if(Turn.unit.onTurnBegin != null) Turn.unit.onTurnBegin();
        
        yield return null;
        if(Turn.unit.GetStat(StatsEnum.HP)<=0){
            if(Turn.unit.active)
                Turn.unit.animationController.Death();

            Turn.unit.active = false;
            machineState.ChangeTo<TurnEndState>();
        }

        machineState.ChangeTo<ChooseActionState>();
    }

    void BreakDraw(){
        for (int i = 0; i < machineState.units.Count-1; i++){
            if(machineState.units[i].chargeTime == machineState.units[i+1].chargeTime)
                machineState.units[i+1].chargeTime+=1;
        }
    }
}
