using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSerealized
{

   public UnitSerealized(string characterName, string spriteModel, Vector3Int position, int faction, int level, PlayerType playerType){
      this.characterName = characterName;
      this.spriteModel = spriteModel;
      this.position = position;
      this.faction = faction;
      this.level = level;
      this.playerType = playerType;
   }
   public string characterName;
   public string spriteModel;
   public Vector3Int position;
   public int faction;
   public int level;
   public PlayerType playerType;
}
