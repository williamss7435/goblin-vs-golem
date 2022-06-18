using UnityEngine;

public class TileLogic {
    
    public Vector3Int pos;
    public Vector3 worldPos;
    public GameObject content;
    //public Floor floor;
    public int contentOrder;
    #region pathfinding
    public TileLogic prev;
    public float distance;
    
    #endregion
    public TileLogic(){}

    public TileLogic(Vector3Int pos, Vector3 worldPos){
        this.pos = pos;
        this.worldPos = worldPos;
        //this.floor = floor;
    }

    public char GetDirection(TileLogic t2){
        if(this.pos.x < t2.pos.x)
            return 'R';
        if(this.pos.x > t2.pos.x)
            return 'L';
            
        return '0';
    }

      public char GetDirection(Vector3Int t2){
        if(this.pos.y < t2.y)
            return 'R';
        if(this.pos.x > t2.x)
            return 'L';
            
        return '0';
    }
}