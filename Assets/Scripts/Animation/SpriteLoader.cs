using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SpriteLoader : MonoBehaviour
{
   public static readonly List<string> animationsNames = new List<string>(){
      "Idle", "Walk", "Attack", "Hit", "Death"
   };
   public List<Animation2D> animations;
   public Dictionary<string, Animation2D> animationsFinder;
   public float framerate;
   public float delayStart;
   public static Transform holder;

   private void Awake() {
      if(holder==null)
         holder = this.transform.parent;
   }
   private void Start() {
      
   }

   public IEnumerator Load(){
      animations = new List<Animation2D>();
      animationsFinder = new Dictionary<string, Animation2D>();

      IList<object> keys = new List<object>();

      animationsNames.ForEach(key => {
         keys.Add(key);
      });

      foreach(string animationName in animationsNames){
         string tempName = transform.name+animationName;

         Animation2D animation2D = new Animation2D();
         animation2D.frameRate = this.framerate;
         animation2D.delayStart = this.delayStart;
         animation2D.name = animationName;
         animation2D.frames = new List<Sprite>();

         animations.Add(animation2D);
         animationsFinder.Add(animation2D.name, animation2D);
         
         yield return Addressables.LoadAssetsAsync<Sprite>(tempName, callback);
         Sprite tempSprite = animation2D.frames[animation2D.frames.Count-1];
         animation2D.frames.Remove(tempSprite);
         animation2D.frames.Insert(0, tempSprite);
      };
   }
   void callback(Sprite obj){
      if(obj != null){
         animations[animations.Count-1].frames.Add(obj);
      }
   }

   public Animation2D GetAnimation(string name){
        Animation2D animation= null;
        animationsFinder.TryGetValue(name, out animation);
        return animation;
    }

   /* public List<string> pathToLoad;
   public List<Sprite> sprites;
   private void Start() {
      IList<object> keys = new List<object>();
      pathToLoad.ForEach(key => {
         keys.Add(key);
      });

      Addressables.LoadAssetsAsync<Sprite>(keys, callback, Addressables.MergeMode.Union);
   }
   void callback(Sprite obj){
      if(obj != null){
         sprites.Add(obj);
      }
   } */
}
