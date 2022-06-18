using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionState : State
{
    List<TileLogic> tiles;
    public override void Enter()
    {
        base.Enter();
        tiles = Board.instance.Search(Turn.unit.tile, Turn.unit.GetComponent<Moviment>().ValidateMovement);
        tiles.Remove(Turn.unit.tile);
        Board.instance.SelectTiles(tiles, Turn.unit.alliance);

        if(Turn.unit.playerType == PlayerType.Human){
            inputs.OnMove+= OnMoveTileSelector;
            inputs.OnFire+= OnFire;
        }else {
            StartCoroutine(ComputerSelectMoveTarget());
        }
    }

    public override void Exit()
    {
        base.Exit();
        inputs.OnMove-= OnMoveTileSelector;
        inputs.OnFire-= OnFire;
        Board.instance.DeSelectTiles(tiles);
    }

    public void OnFire(object sender, object args){
        int button = (int)args;

        if(button==1){
            if(tiles.Contains(machineState.selectedTile))
                machineState.ChangeTo<MoveSequenceState>();
        }else if (button==2){
            machineState.ChangeTo<ChooseActionState>();
        }
    }

    IEnumerator ComputerSelectMoveTarget(){

        AIPlan plan = ComputerPlayer.instance.currentPlan;
        while(SelectorBoard.instance.position!=plan.movePos){
            if(SelectorBoard.instance.position.x<plan.movePos.x)
                OnMoveTileSelector(null, Vector3Int.right);
            if(SelectorBoard.instance.position.x>plan.movePos.x)
                OnMoveTileSelector(null, Vector3Int.left);
            if(SelectorBoard.instance.position.y<plan.movePos.y)
                OnMoveTileSelector(null, Vector3Int.up);
            if(SelectorBoard.instance.position.y>plan.movePos.y)
                OnMoveTileSelector(null, Vector3Int.down);

            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);
        machineState.ChangeTo<MoveSequenceState>();
    }
}
