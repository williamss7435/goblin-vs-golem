using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSkillRange : SkillRange
{
    public override List<TileLogic> GetTilesInRange(){
        return new List<TileLogic>(Board.instance.tiles.Values);
    }
}
