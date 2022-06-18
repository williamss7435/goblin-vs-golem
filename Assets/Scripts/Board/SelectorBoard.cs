using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBoard : MonoBehaviour
{
   public static SelectorBoard instance;
   public Vector3Int position{get{return tile.pos;}}
   public TileLogic tile;
   public SpriteRenderer spriteRenderer;
   public List<Sprite> sprites;
   private void Awake() {
       instance = this;
       spriteRenderer = GetComponentInChildren<SpriteRenderer>();
   }
   
   public void ChangeSpriteAllySelector(){
        ChangeSprite(0);
   }

   public void ChangeSpriteEnemySelector(){
        ChangeSprite(1);
   }

   void ChangeSprite(int index){
        this.spriteRenderer.sprite = sprites[index];
   }
   public void Show(){
        spriteRenderer.color = new Color(255,255,255,255);
   }
   public void Hide(){
        spriteRenderer.color = new Color(255,255,255,0);
   }
}
