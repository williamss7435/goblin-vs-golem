using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(MoveSequence());
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator MoveSequence(){
        List<TileLogic> path = CreatePath();
        path.Add(machineState.selectedTile);
        Moviment movement = Turn.unit.GetComponent<Moviment>();
        
        yield return StartCoroutine(movement.Move(path));
        Turn.unit.tile.content = null;
        Turn.unit.tile = machineState.selectedTile;
        Turn.unit.tile.content = Turn.unit.gameObject;
        yield return new WaitForSeconds(0.2f);
        Turn.hasMoved = true;

        machineState.ChangeTo<ChooseActionState>();
    }
    List<TileLogic> CreatePath(){
        List<TileLogic> path = new List<TileLogic>();

        TileLogic t = machineState.selectedTile;
        while(t!=Turn.unit.tile){
            path.Add(t);
            t = t.prev;
        };
        path.Reverse();
        return path;
    }
}
