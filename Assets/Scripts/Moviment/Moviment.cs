using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    const float MoveSpeed = 0.5f;
    SpriteRenderer SR;
    TileLogic tileAtual;
    
    private void Awake() {
        SR = GetComponentInChildren<SpriteRenderer>();
    }

    IEnumerator Walk(TileLogic to){
        int id = LeanTween.move(transform.gameObject, to.worldPos, MoveSpeed).id;
        tileAtual = to;
        
        while(LeanTween.descr(id)!= null)
            yield return null;
    }

    public IEnumerator Move(List<TileLogic> path){
      tileAtual = Turn.unit.tile;
      tileAtual.content = null;
     
      char direction = tileAtual.GetDirection(path[path.Count - 1]);
      if(direction != '0')
        Turn.unit.ChangeDirection(direction);
    
      for(int i = 0; i<path.Count; i++){
          TileLogic to = path[i];
        Turn.unit.animationController.Walk();
          if(i == path.Count-1){
              Turn.unit.animationController.Blink();
          }
          yield return Walk(to);
      }
      Turn.unit.animationController.Idle();
    }

    public virtual bool ValidateMovement(TileLogic from, TileLogic to){
        to.distance = from.distance+1;

        if(to.content != null || to.distance>Turn.unit.GetStat(StatsEnum.MOV)) 
          return false;

        return true;
    }

}
