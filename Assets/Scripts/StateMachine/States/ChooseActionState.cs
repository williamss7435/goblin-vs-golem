using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseActionState : State
{ 
    
    public override void Enter()
    {
        base.Enter();
        MoveSelector(Turn.unit.tile);
        CameraController.instance.setUnit(Turn.unit);
        if(Turn.unit.playerType == PlayerType.Human){
            if(Turn.hasMoved) index = 1;
            else index = 0;
            ChangeChooseActionText();
            
            SelectorBoard.instance.ChangeSpriteAllySelector();
            currentUISelector = machineState.chooseActionSelection;
            SelectorBoard.instance.Show();
            ChangeIUSelector(machineState.chooseActionButtons);
            CheckActions();
            InputController.instance.OnMove += OnMove;
            InputController.instance.OnFire += OnFire;
            machineState.chooseActionPanel.MoveTo("Show");
        }else {
            SelectorBoard.instance.ChangeSpriteEnemySelector();
            StartCoroutine(ComputerChooseAction());
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        InputController.instance.OnMove -= OnMove;
        InputController.instance.OnFire -= OnFire;
        machineState.chooseActionPanel.MoveTo("Hide");
    }

    public void OnMove(object sender, object args){
        Vector3Int button = (Vector3Int)args;
        if(button == Vector3Int.left){
            index--;
            ChangeIUSelector(machineState.chooseActionButtons);
        }else if(button == Vector3Int.right) {
            index++;
            ChangeIUSelector(machineState.chooseActionButtons);
        }

        ChangeChooseActionText();
    }

    void ChangeChooseActionText(){

        if(index == 0 && !Turn.hasMoved)
            machineState.txtActionState.text = "Move Unit";
        else if(index == 1 && !Turn.hasActed)
            machineState.txtActionState.text = "Select Skill";
        else if(index == 2)
            machineState.txtActionState.text = "End Turn";
        else
            machineState.txtActionState.text = "";
    }
    
    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1){
            ActionButtons();
        }else if (button==2){
            StateMachineController.instance.ChangeTo<RoamState>();
        }
    }

    public void ActionButtons(){
        switch(index){
            case 0:
                if(!Turn.hasMoved) machineState.ChangeTo<MoveSelectionState>();
                break;
            case 1:
                if(!Turn.hasActed) machineState.ChangeTo<SkillSelectionState>();
                break;
            case 2:
               machineState.ChangeTo<TurnEndState>();
                break;
        }
    }

    void CheckActions(){
        PaintButton(machineState.chooseActionButtons[0], Turn.hasMoved);
        PaintButton(machineState.chooseActionButtons[1], Turn.hasActed);
        PaintButton(machineState.chooseActionButtons[2], Turn.hasActed);
    }

    void PaintButton(Image image, bool check){
        image.color = check ? new Color(255, 255, 255, 0.2f) : Color.white;
    }

    IEnumerator ComputerChooseAction(){
        AIPlan plan = ComputerPlayer.instance.currentPlan;
        if(plan == null){
            plan = ComputerPlayer.instance.Evaluate();
            Turn.skill = plan.skill;
        }

        yield return new WaitForSeconds(1f);

        if(Turn.hasMoved == false && plan.movePos != Turn.unit.tile.pos){
            machineState.ChangeTo<MoveSelectionState>();
        }else if (Turn.hasActed == false && Turn.skill != null){
            machineState.ChangeTo<SkillTargetState>();
        }else {
            machineState.ChangeTo<TurnEndState>();
        }
    }
}
