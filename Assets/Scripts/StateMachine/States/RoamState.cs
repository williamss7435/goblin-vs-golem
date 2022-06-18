using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
    public override void Enter()
    {
        base.Enter();
        inputs.OnMove+= OnMoveTileSelector;
        inputs.OnFire+= OnFire;
        CheckNullPosition();
        //SelectorBoard.instance.Show();
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove-= OnMoveTileSelector;
        inputs.OnFire-= OnFire;
        //SelectorBoard.instance.Hide();
    }

    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1){

        }else if (button==2){
            machineState.ChangeTo<ChooseActionState>();
        }
    }

    public void CheckNullPosition(){
        if(SelectorBoard.instance.tile == null)
            SelectorBoard.instance.tile = Board.GetTile(Vector3Int.zero);

        
        if(SelectorBoard.instance.position == null)
            SelectorBoard.instance.transform.position = SelectorBoard.instance.tile.worldPos;
        
    }
    
}
