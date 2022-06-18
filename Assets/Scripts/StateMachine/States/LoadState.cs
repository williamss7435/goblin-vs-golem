using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : State
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(LoadSequecence());
    }

    IEnumerator LoadSequecence(){
        yield return StartCoroutine(Board.instance.InitSequence(this));
        yield return null;
        yield return StartCoroutine(LoadAnimations());
        yield return null;
        MapLoader.instance.CreateUnits();
        yield return null;
        InitialTurnOrdering();
        yield return null;
        UnitAlliances();
        yield return null;
        List<Vector3Int> blockers = Blockers.instance.GetBlockers();
        yield return null;
        SetBlockers(blockers);
        yield return null;
        StateMachineController.instance.ChangeTo<TurnBeginState>();
    }

    public void InitialTurnOrdering(){
        for(int i=0; i<machineState.units.Count; i++){
            Unit unit = machineState.units[i];
            unit.chargeTime = 100 - unit.GetStat(StatsEnum.SPEED); 
            unit.active = true;
        }
    }

    public void UnitAlliances(){
        for (int i = 0; i < machineState.units.Count; i++)
            SetUnitAlliance(machineState.units[i]);
    }

    void SetUnitAlliance(Unit unit){
        for (int i = 0; i < MapLoader.instance.alliances.Count; i++){
            if(MapLoader.instance.alliances[i].factions.Contains(unit.faction)){
                MapLoader.instance.alliances[i].units.Add(unit);
                unit.alliance = i;
                return;
            }
        }
    }

    void SetBlockers(List<Vector3Int> blockers){
        foreach (Vector3Int pos in blockers){
            TileLogic t = Board.GetTile(pos);
            t.content = Blockers.instance.gameObject;
        }
    }

    IEnumerator LoadAnimations(){
        SpriteLoader[] loaders = SpriteLoader.holder.GetComponentsInChildren<SpriteLoader>();
        foreach(SpriteLoader spriteLoader in loaders){
            yield return spriteLoader.Load();
        }
    }
}
