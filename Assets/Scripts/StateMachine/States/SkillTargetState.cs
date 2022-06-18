using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetState : State
{
    List<TileLogic> selectedTiles;
    public override void Enter()
    {
        base.Enter();

        selectedTiles = Turn.skill.GetTargets();
        board.SelectTiles(selectedTiles, Turn.unit.alliance);

        if(Turn.unit.playerType == PlayerType.Human){
            inputs.OnMove+= OnMoveTileSelector;
            inputs.OnFire+= OnFire;
        }else {
            StartCoroutine(ComputerTargeting());
        }
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove-= OnMoveTileSelector;
        inputs.OnFire-= OnFire;
        
        board.DeSelectTiles(selectedTiles);
    }

    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1 && selectedTiles.Contains(SelectorBoard.instance.tile)){
           
            Turn.targets = selectedTiles;
            machineState.ChangeTo<ConfirmSkillState>();

        }else if (button==2){
            machineState.ChangeTo<SkillSelectionState>();
        }
    }

    IEnumerator ComputerTargeting(){
        SkillRange sr = Turn.skill.GetComponentInChildren<SkillRange>();
        AIPlan plan = ComputerPlayer.instance.currentPlan;

        yield return StartCoroutine(AIMoveSelector(plan.movePos));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(SearchEnemy());
        Turn.targets = selectedTiles;
        yield return new WaitForSeconds(0.7f);
        
        machineState.ChangeTo<ConfirmSkillState>();
    }
}
