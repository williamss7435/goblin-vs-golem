using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    protected int index;
    protected Image currentUISelector;
    protected InputController inputs{get{return InputController.instance;}}
    protected StateMachineController machineState{get{return StateMachineController.instance;}}
    protected Board board{get{return Board.instance;}}
    public virtual void Enter(){
        
    }

    public virtual void Exit(){
        
    }

    protected void OnMoveTileSelector(object sender, object args){
        Vector3Int input = (Vector3Int)args;
        TileLogic t = Board.GetTile(SelectorBoard.instance.position + input);
        if(t!=null)
            MoveSelector(t);
    }

    protected void MoveSelector(Vector3Int position){
        MoveSelector(Board.GetTile(position));
    }

    protected void MoveSelector(TileLogic tile){
        machineState.selectedTile = tile;
        SelectorBoard.instance.tile = tile;
        SelectorBoard.instance.transform.position = tile.worldPos; 
    }

    protected void ChangeIUSelector(List<Image> buttons){
        if(index<0){
            index = buttons.Count-1;
        }else if (index==buttons.Count){
            index = 0;
        }

        currentUISelector.transform.localPosition = 
        buttons[index].transform.localPosition;
    }

    protected IEnumerator AIMoveSelector(Vector3Int destination){
        while(SelectorBoard.instance.position!=destination){
            if(SelectorBoard.instance.position.x<destination.x)
                OnMoveTileSelector(null, Vector3Int.right);
            if(SelectorBoard.instance.position.x>destination.x)
                OnMoveTileSelector(null, Vector3Int.left);
            if(SelectorBoard.instance.position.y<destination.y)
                OnMoveTileSelector(null, Vector3Int.up);
            if(SelectorBoard.instance.position.y>destination.y)
                OnMoveTileSelector(null, Vector3Int.down);
            yield return new WaitForSeconds(0.25f);
        }
    }
    protected IEnumerator SearchEnemy(){
            List<Vector3Int> positions = new List<Vector3Int>();
            Unit unit;

            positions.Add(Turn.unit.tile.pos+Vector3Int.left);
            positions.Add(Turn.unit.tile.pos+Vector3Int.right);
            positions.Add(Turn.unit.tile.pos+Vector3Int.up);
            positions.Add(Turn.unit.tile.pos+Vector3Int.down);
            
            foreach(Vector3Int position in positions){
                TileLogic tile = Board.GetTile(position);
                if(tile != null && tile.content != null){
                    unit = tile.content.GetComponent<Unit>();
                    if(unit != null && unit.alliance != Turn.unit.alliance){
                        MoveSelector(position);
                    }
                }
            }

            yield return null;
    }
}
