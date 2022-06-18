using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
   public static ComputerPlayer instance;
   Unit currentUnit{get{ return Turn.unit;}}
   int alliance{get{ return currentUnit.alliance;}}
   Unit nearestFoe;
   public AIPlan currentPlan;
   private void Awake() {
       instance = this;
   }

   public AIPlan Evaluate(){
       AISkillBehaviour aISkillBehavior = Turn.unit.GetComponent<AISkillBehaviour>();
       if(aISkillBehavior == null){
           aISkillBehavior = Turn.unit.gameObject.AddComponent<AISkillBehaviour>();
       }
       AIPlan plan = new AIPlan();
       if(TryToEvaluate(plan, aISkillBehavior)){
           //plan.movePos = Turn.unit.tile.pos;
       } 
       
       if(plan.skill == null){
           MoveTowardOpponent(plan);
        }

       currentPlan = plan;
       return plan;
   }

   bool TryToEvaluate(AIPlan plan, AISkillBehaviour skillBehaviour){
        skillBehaviour.Pick(plan);

        if(plan.skill == null){
            Debug.Log("Did not choose any skill");
            return false;
        }
        
        Debug.Log("Choose skill: "+ plan.skill);
        PlanDirection(plan);
    
        return true;
    }

    void PlanDirection(AIPlan plan){
        TileLogic startTile = Turn.unit.tile;
        char startDirection = Turn.unit.direction;
        TileLogic selectorStartTile = SelectorBoard.instance.tile;
        List<AttackOption> list = new List<AttackOption>();
        List<TileLogic> moveOptions = GetMoveOptions(true);
        char[] directions = new char[]{'L', 'R'};

        for(int i=0; i<moveOptions.Count; i++){
            TileLogic moveTile = moveOptions[i];
            Turn.unit.tile.content = null;
            Turn.unit.tile = moveTile;
            moveTile.content = Turn.unit.gameObject;
            for(int j=0; j<2; j++){
                Turn.unit.direction = directions[j];
                AttackOption ao = new AttackOption();
                ao.target = moveTile;
                ao.direction = Turn.unit.direction;
                RateFireLocation(plan, ao);
                ao.moveTargets.Add(moveTile);
                list.Add(ao);
            }
        }

        Turn.unit.tile.content = null;
        Turn.unit.tile = startTile;
        startTile.content = Turn.unit.gameObject;
        Turn.unit.direction = startDirection;
        SelectorBoard.instance.tile = selectorStartTile;
        PickBestOption(plan, list);
        
        PrintAttackOptionsList(list);
        Debug.LogFormat("Move to {0} and use {1}",
            plan.movePos, plan.skill);
    }

    void PrintAttackOptionsList(List<AttackOption> list){
        foreach (AttackOption ao in list)
        {
            foreach(AttackOption.Hit hit in ao.hits){
                Debug.LogFormat("Hit: {0} {1}", hit.tile.pos, hit.isMatch);
            }
        }
    }

    void MoveTowardOpponent(AIPlan plan){
        List<TileLogic> moveOptions = GetMoveOptions();
        FindNearestFoe();
        if(nearestFoe!=null){
            TileLogic toCheck = nearestFoe.tile;
            while(toCheck!=null){
                if(moveOptions.Contains(toCheck)){
                    plan.movePos = toCheck.pos;
                    return;
                }
                toCheck = toCheck.prev;
            }
        }
        plan.movePos = Turn.unit.tile.pos;
    }

    List<TileLogic> GetMoveOptions(){
        return Board.instance.Search(Turn.unit.tile, Turn.unit.GetComponent<Moviment>().ValidateMovement);
    }
    List<TileLogic> GetMoveOptions(bool includeCurrentPosition){
        List<TileLogic> rtv = GetMoveOptions();
        rtv.Add(Turn.unit.tile);
        return rtv;
    }

   void FindNearestFoe(){
        nearestFoe = null;
        Board.instance.Search(Turn.unit.tile, delegate(TileLogic arg1, TileLogic arg2){
            if(nearestFoe == null && arg2.content !=null){
                Unit unit = arg2.content.GetComponent<Unit>();
                if(unit!=null && currentUnit.alliance!= unit.alliance){
                    Stats stats = unit.stats;
                    if(stats[StatsEnum.HP].currentValue >0){
                        nearestFoe = unit;
                        return true;
                    }
                }
            }
            arg2.distance = arg1.distance+1;
            return nearestFoe == null;
        });
    }

    void RateFireLocation(AIPlan plan, AttackOption option){
        List<TileLogic> tiles = plan.skill.GetTargets();
    
        option.areaTargets = tiles;
        option.isCasterMatch = IsSkillTargetMatch(plan, Turn.unit.tile, Turn.unit);

        for(int i=0; i<tiles.Count; i++){
            TileLogic tile = tiles[i];
            if(Turn.unit.tile == tile || tile.content ==null)
                continue;
            Unit unit = tile.content.GetComponent<Unit>();
            if(unit == null || unit.stats[StatsEnum.HP].currentValue<=0)
                continue;
            bool isMatch = IsSkillTargetMatch(plan, tile, unit);
            option.AddHit(tile, isMatch);
        }
    }
    bool IsSkillTargetMatch(AIPlan plan, TileLogic tile, Unit unit){
        if(unit==null)
            return false;
        
        switch(plan.targetType){
            case SkillAffectsType.Default:
                return true;
            case SkillAffectsType.EnemyOnly:
                if(unit.alliance != Turn.unit.alliance)
                    return true;
                break;
            case SkillAffectsType.AllyOnly:
                if(unit.alliance == Turn.unit.alliance)
                    return true;
                break;
        }
        return false;
    }
    
    void PickBestOption(AIPlan plan, List<AttackOption> list){
        int bestScore = 1;
        List<AttackOption> bestOptions = new List<AttackOption>();
        for(int i=0; i<list.Count; i++){
            AttackOption option = list[i];
            int score = option.GetScore(Turn.unit, plan.skill);
            if(score > bestScore){
                bestScore = score;
                bestOptions.Clear();
                bestOptions.Add(option);
            }else if(score == bestScore){
                bestOptions.Add(option);
            }
        }

        if(bestOptions.Count == 0){
            plan.skill = null;
            return;
        }

        AttackOption choice = bestOptions[Random.Range(0, bestOptions.Count)];

        plan.skillTargetPos = choice.target.pos;
        plan.direction = choice.direction;
        plan.movePos = choice.bestMoveTile.pos;
    }
}
