using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformSkillState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PerformSequence());
    }

    IEnumerator PerformSequence(){
        yield return null;

        char direction = Turn.unit.tile.GetDirection(SelectorBoard.instance.tile);
        if(direction != '0') Turn.unit.ChangeDirection(direction);
        
        Turn.skill.Perform();
        if(Turn.unit.playerType == PlayerType.Human){
            Turn.unit.SetStat(StatsEnum.MP, -Turn.skill.manaCost);
        }
        yield return new WaitForSeconds(1);
        
        machineState.ChangeTo<TurnEndState>();
    }
}
