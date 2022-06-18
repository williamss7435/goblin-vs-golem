using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSkillState : State
{
     public override void Enter(){
        base.Enter();
        machineState.skillPredictionPanel.SetPredictionText();
        machineState.skillPredictionPanel.positioner.MoveTo("Show");

        if(Turn.unit.playerType == PlayerType.Human){
            board.SelectTile(SelectorBoard.instance.tile, Turn.unit.alliance);
            inputs.OnFire+= OnFire;
        }else{
            machineState.leftcharacterPanel.Show(Turn.unit);
            StartCoroutine(ComputerConfirmSkill());
        }

    }

    public override void Exit(){
        base.Exit();
        inputs.OnFire-= OnFire;
        machineState.skillPredictionPanel.positioner.MoveTo("Hide");
        machineState.rightcharacterPanel.Hide();
        board.DeSelectTiles(Turn.targets);
    }

    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1){
            if(Turn.skill.ValidateTarget(SelectorBoard.instance.tile)){
                machineState.leftcharacterPanel.Hide();
                machineState.ChangeTo<PerformSkillState>();
            }else{
                Debug.Log("No Unit would be affected");
            }
        }else if (button==2){
            machineState.ChangeTo<SkillTargetState>();
        }
    }

    IEnumerator ComputerConfirmSkill(){
        yield return new WaitForSeconds(1.5f);
        machineState.leftcharacterPanel.Hide();
        machineState.ChangeTo<PerformSkillState>();
    }
}
