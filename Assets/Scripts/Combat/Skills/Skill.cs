using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
   public string skillName;
   [Multiline]
   public string skillDescription;
   public int manaCost;
   public Sprite icon;
   Transform _primary;
   Transform primary {
      get{
         if(_primary==null){
            _primary = transform.Find("Primary");
            secondary = transform.Find("Secondary");
         }
         return _primary;
      }
   }
   Transform secondary;
   
   public bool CanUse(){
      if(Turn.unit.GetStat(StatsEnum.MP)>=manaCost) return true;
      return false;
   }

   public bool ValidateTarget(List<TileLogic> targets){
      foreach(TileLogic t  in targets){
         if(
            t.content!= null && 
            t.content.GetComponent<Unit>()!=null && 
            GetComponentInChildren<SkillAffects>().IsTarget(t.content.GetComponent<Unit>())
         ){
            return true;
         }
      }
      return false;
   }

   public bool ValidateTarget(TileLogic target){
         if(
            target.content!= null && 
            target.content.GetComponent<Unit>()!=null && 
            GetComponentInChildren<SkillAffects>().IsTarget(target.content.GetComponent<Unit>())
         ){
            return true;
         }
      
      return false;
   }

   public List<TileLogic> GetTargets(){
      return GetComponentInChildren<SkillRange>().GetTilesInRange();
   }

   public int GetHitPrediction(Unit target){
      return primary.GetComponentInChildren<HitRate>().Predict(target);
   }

   public int GetHitPrediction(Unit target, Transform effect){
      return effect.GetComponentInChildren<HitRate>().Predict(target);
   }

   public int GetEffetPrediction(Unit target){
      return primary.GetComponentInChildren<SkillEffect>().Predict(target);
   }

   public int GetEffetPrediction(Unit target, Transform effect){
      return effect.GetComponentInChildren<SkillEffect>().Predict(target);
   }

   public void Perform(){
      FilterContent();

      for (int i = 0; i < Turn.targets.Count; i++){
         Unit unit = Turn.targets[i].content.GetComponent<Unit>();

         if(unit!=null){
            bool didHit = RollToHit(unit, primary);
            if(didHit){
               VFX(unit, didHit);
               SFX();
               primary.GetComponentInChildren<SkillEffect>().Apply(unit);
               if(secondary.childCount!=0 && RollToHit(unit, secondary)){
                  secondary.GetComponentInChildren<SkillEffect>().Apply(unit);
               }
            }
         }

      }  
   }
   void FilterContent(){
      Turn.targets.RemoveAll(x => x.content == null || SelectorBoard.instance.position != x.pos);
   }

   public bool RollToHit(Unit unit, Transform effect){
      return effect.GetComponentInChildren<HitRate>().TryHit(unit);
   }

   void VFX (Unit target, bool didHit){
      SkillVisualFX fx = GetComponentInChildren<SkillVisualFX>();
      if(fx!=null){
         fx.target = target;
         fx.didHit = didHit;
         fx.VFX();
      }
   }

   void SFX(){
      SkillSoundFX fx = GetComponentInChildren<SkillSoundFX>();
      if(fx!=null){
         fx.Play();
      }
   }
}
