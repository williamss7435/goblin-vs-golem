using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOption 
{
   public class Hit{
        public TileLogic tile;
        public bool isMatch;

        public Hit(TileLogic tile, bool isMatch){
            this.tile = tile;
            this.isMatch = isMatch;
        }
    }
    public TileLogic target;
    public char direction;
    public List<TileLogic> areaTargets = new List<TileLogic>();
    public bool isCasterMatch;
    public TileLogic bestMoveTile;
    public List<Hit> hits = new List<Hit>();
    public List<TileLogic> moveTargets = new List<TileLogic>();
    public void AddHit (TileLogic tile, bool isMatch)
	{
		hits.Add (new Hit(tile, isMatch));
	}
    public int GetScore(Unit caster, Skill skill){
        GetBestMoveTarget(caster, skill);
        if(bestMoveTile == null)
            return 0;
        
        int score = 0;
        for(int i=0; i<hits.Count; i++){
            if(hits[i].isMatch)
                score++;
            else
                score--;
        }
        if(isCasterMatch && areaTargets.Contains(bestMoveTile))
            score++;
        
        return score;
    }
    void GetBestMoveTarget(Unit caster, Skill skill){
        if(moveTargets.Count == 0)
            return;
        bestMoveTile = moveTargets[Random.Range(0, moveTargets.Count)];
    }
}
