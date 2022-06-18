using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSkillRange : SkillRange
{
    public override List<TileLogic> GetTilesInRange()
    {
        Unit unit = Turn.unit;
        Vector3Int startPos = unit.tile.pos;
        List<Vector3Int> directions = new List<Vector3Int>{
            new Vector3Int(0,1,0), new Vector3Int(0,-1,0),
            new Vector3Int(1,0,0), new Vector3Int(-1,0,0)
        };
        List<TileLogic> retValue = new List<TileLogic>();        

        directions.ForEach(direction => {
            Vector3Int currentPos = startPos;

            for (int i = 1; i <= range; i++){
                currentPos+=direction;
                TileLogic t = Board.GetTile(currentPos);

                if(t!=null)
                    retValue.Add(t);
            }
        });
       
        return retValue;
    }
}
