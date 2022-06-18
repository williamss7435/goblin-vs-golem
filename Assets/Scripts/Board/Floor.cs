using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{

    [HideInInspector]
    public TilemapRenderer tilemapRenderer;
    [HideInInspector]
    public Tilemap tilemap;

    public int order{get{return tilemapRenderer.sortingOrder;}}
    public int contentOrder;
    public Vector3Int minX;
    public Vector3Int maxX;
    public Tilemap hightLight;
    private void Awake() {
        tilemapRenderer = this.transform.GetComponent<TilemapRenderer>();
        tilemap = GetComponent<Tilemap>();
        hightLight = this.transform.Find("Highlight").GetComponent<Tilemap>();
    }

    public List<Vector3Int> Loadtiles(){
        List<Vector3Int> tiles = new List<Vector3Int>();
        Vector3Int currentPosition;
        
        for(int i=minX.x; i <= maxX.x; i++){
            for(int j=minX.y; j<= maxX.y; j++){
              
                currentPosition = new Vector3Int(i, j, 0);
                if(tilemap.HasTile(currentPosition)) {
                    tiles.Add(currentPosition);
                } 
                    
                
            }
        }
            
        return tiles;
    }
}
