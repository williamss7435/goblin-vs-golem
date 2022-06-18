using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Blockers : MonoBehaviour
{
  public static Blockers instance;
  private void Awake() {
      instance = this;
      GetComponent<TilemapRenderer>().enabled = false;
  }

   public List<Vector3Int> GetBlockers(){
       Tilemap tilemap = GetComponent<Tilemap>();
       List<Vector3Int> blockeds = new List<Vector3Int>();

       BoundsInt bounds = tilemap.cellBounds;

       foreach (var pos in bounds.allPositionsWithin){
           if(tilemap.HasTile(pos))
               blockeds.Add(new Vector3Int(pos.x, pos.y, 0));
       }

       return blockeds;
   }
}
