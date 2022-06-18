using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Unit unit;
    SpriteSwapper SS;
    void Awake() {
        unit = GetComponent<Unit>();
        SS = transform.Find("Sprite").GetComponent<SpriteSwapper>();
    }

    public void Idle(){
        Play("Idle");
    }

    public void Walk(){
        Play("Walk");
    }

    public void Attack(){
        SS.PlayThenReturn("Attack");
    }

    public void Hit(){
        SS.PlayThenReturn("Hit");
    }
    public void Hit(float delay){
        Invoke("Hit", delay);
    }

    public void Death(){
        SS.PlayThenStop("Death");
    }

    public void Death(float delay){
        Invoke("Death", delay);
    }
    public void Blink(){
        StartCoroutine(SS.Blink());
    }

    void Play(string animationName){
        if(SS.current.name!=animationName)
            SS.PlayAnimation(animationName);
    }

    public float GetAnimationTimer(string animationName){
        Animation2D animation = SS.thisUnitSprites.GetAnimation(animationName);
        float timePerFrame = 1/animation.frameRate;
        return animation.frames.Count*timePerFrame;
    }
}
