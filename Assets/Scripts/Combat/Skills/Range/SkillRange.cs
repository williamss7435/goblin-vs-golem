using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillRange : MonoBehaviour
{
   public int range;
   public int verticalRange;
   public virtual bool isDirectionOriented(){
      return false;
   }
   public virtual char GetDirection(){
      if(Turn.targets[0] == Turn.unit.tile) return Turn.unit.direction;

      return Turn.unit.tile.GetDirection(Turn.targets[0]);
   }
   public abstract List<TileLogic> GetTilesInRange();
}
