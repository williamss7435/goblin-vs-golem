using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionState : State
{
    List<Skill> skills;
    public override void Enter()
    {
        base.Enter();
        index = 0;
        InputController.instance.OnMove += OnMove;
        InputController.instance.OnFire += OnFire;
        currentUISelector = machineState.skillSelectionSelection;
        machineState.skillSelectionPanel.MoveTo("Show");
        machineState.leftcharacterPanel.Show(Turn.unit);
        ChangeIUSelector(machineState.skillSelectionButtons);
        CheckSkills();
        SetSkillDescription();
    }

    public override void Exit()
    {
        base.Exit();
        InputController.instance.OnMove -= OnMove;
        InputController.instance.OnFire -= OnFire;
        machineState.skillSelectionPanel.MoveTo("Hide");
    }

    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1){
            ActionButtons();
        }else if (button==2){
            machineState.leftcharacterPanel.Hide();
            StateMachineController.instance.ChangeTo<ChooseActionState>();
        }
    }

    public void ActionButtons(){
        if(index>=skills.Count) return;

        if(skills[index].CanUse()){
            Debug.Log("Using " + skills[index].name);
            Turn.skill = skills[index];
            machineState.ChangeTo<SkillTargetState>();
        }else {
            StartCoroutine(LoadPanel.instance.ShowMessage("Not Enough Mana"));
        }
    }

    public void OnMove(object sender, object args){
        Vector3Int button = (Vector3Int)args;
        if(button == Vector3Int.up){
            index--;
            ChangeIUSelector(machineState.skillSelectionButtons);
        }else if(button == Vector3Int.down) {
            index++;
            ChangeIUSelector(machineState.skillSelectionButtons);
        }
        SetSkillDescription();
    }

    void SetSkillDescription(){
        if(skills != null && index+1 <= skills.Count){
            machineState.attackDescriptionPanel.ShowDescription(skills[index].skillName, skills[index].skillDescription);
        }else {
            machineState.attackDescriptionPanel.Hide();
        }
        
    }
    void CheckSkills(){
        Transform skillBook = Turn.unit.transform.Find("SkillBook");
        

        skills = new List<Skill>();
        skills.AddRange(skillBook.GetComponent<SkillBook>().skills);

        for (int i = 0; i < 5; i++){
            if(i<skills.Count){
                machineState.skillSelectionButtons[i].sprite = skills[i].icon;
            }else{
                machineState.skillSelectionButtons[i].sprite = machineState.skillSelectionBlock;
            }
        }
    }
}
