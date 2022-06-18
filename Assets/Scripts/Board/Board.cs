using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
public class Board : MonoBehaviour {
    public static Board instance;

    public Floor floor;
    public Dictionary<Vector3Int, TileLogic> tiles;
    public Grid grid;
    public List<Tile> highlights;
    public Vector3Int[] dirs = new Vector3Int[4]{
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
    private void Awake() {
        instance = this;
        tiles = new Dictionary<Vector3Int, TileLogic>();
        grid = GetComponent<Grid>();
        floor = GetComponentInChildren<Floor>();
    }

    public IEnumerator InitSequence(LoadState loadState){
        yield return StartCoroutine(LoadTiles());
        yield return null;
    }

    public IEnumerator LoadTiles(){
        List<Vector3Int> floorTiles = floor.Loadtiles();
        for (int i = 0; i < floorTiles.Count; i++){
            yield return null;
            if(!tiles.ContainsKey(floorTiles[i])){
                CreateTile(floorTiles[i]);
            }
        }
    }

    public void CreateTile(Vector3Int pos){
        Vector3 worldPos = grid.CellToWorld(pos);
        TileLogic tileLogic = new TileLogic(pos, worldPos);
        tiles.Add(pos, tileLogic);
    }

    public static TileLogic GetTile(Vector3Int pos){
        TileLogic tile = null;
        instance.tiles.TryGetValue(pos, out tile);
        return tile;
    }

    public void SelectTiles(List<TileLogic> tiles, int allianceIndex){
        for (int i = 0; i < tiles.Count; i++){
            floor.hightLight.SetTile(tiles[i].pos, highlights[allianceIndex]);
        }
    }

    public void SelectTile(TileLogic tile, int allianceIndex){
            floor.hightLight.SetTile(tile.pos, highlights[allianceIndex]);
    }

    public void DeSelectTiles(List<TileLogic> tiles){
        for (int i = 0; i < tiles.Count; i++){
            floor.hightLight.SetTile(tiles[i].pos, null);
        }
    }

    public List<TileLogic> Search(TileLogic start, Func<TileLogic, TileLogic, bool> searchType){
        List<TileLogic> tilesSearch = new List<TileLogic>();
        //Moviment m = Turn.unit.GetComponent<Moviment>();
        tilesSearch.Add(start);
        ClearSearch();

        Queue<TileLogic> checkNext = new Queue<TileLogic>();
        Queue<TileLogic> checkNow = new Queue<TileLogic>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while(checkNow.Count>0){
            TileLogic t = checkNow.Dequeue();
            for (int i = 0; i < 4; i++){
                TileLogic next = GetTile(t.pos + dirs[i]);

                if(next == null || next.distance<=t.distance+1) 
                    continue;

                if(searchType(t, next)){
                    //next.distance = t.distance+1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    tilesSearch.Add(next);
                }
            }
            if(checkNow.Count == 0)
                SwapReference(ref checkNow, ref checkNext);
        }

        return tilesSearch;
    }

    void SwapReference(ref Queue<TileLogic> now, ref Queue<TileLogic> next){
        Queue<TileLogic> temp = now;
        now = next;
        next = temp;
    }
    void ClearSearch(){
        foreach (TileLogic t in tiles.Values){
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }
}