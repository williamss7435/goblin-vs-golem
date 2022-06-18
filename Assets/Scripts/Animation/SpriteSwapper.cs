using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    public SpriteLoader thisUnitSprites;
    public SpriteRenderer SR;

    public Animation2D current;
    public Coroutine playing;
    public Queue<Animation2D> sequence;
    private void Awake() {
        SR = GetComponent<SpriteRenderer>();
        sequence = new Queue<Animation2D>();
    }
    IEnumerator Play(){
        while(true){
            current = sequence.Dequeue();

            if(sequence.Count== 0){
                sequence.Enqueue(current);
            }

            float timePerFrame = 1/current.frameRate; 
            for (int i = 0; i < current.frames.Count; i++)
            {
                SR.sprite = current.frames[i];
                yield return new WaitForSeconds(timePerFrame);
            }
        }
    }

    public void Stop(){
        if(playing!=null){
            StopCoroutine(playing);
        }
        sequence.Clear();
    }

    public IEnumerator Blink(){
        float MoveSpeed = 0.5f;
        int id = LeanTween.color(SR.gameObject, new Color(255, 255, 255, 0), MoveSpeed).id;
    
        while(LeanTween.descr(id)!= null)
            yield return null;

        id = LeanTween.color(SR.gameObject, new Color(255, 255, 255, 1), MoveSpeed).id;
    
        while(LeanTween.descr(id)!= null)
            yield return null;
    
    }

    public void PlayAnimation(string name){
        Stop();
        sequence.Enqueue(thisUnitSprites.GetAnimation(name));
        playing = StartCoroutine(Play());
    }

     public void PlayAnimations(List<string> names){
        Stop();
        foreach(string name in names){
            sequence.Enqueue(thisUnitSprites.GetAnimation(name));
        }
        playing = StartCoroutine(Play());
    }

    public void PlayThenReturn(string name){
        Animation2D toPlay = thisUnitSprites.GetAnimation(name);
        Stop();
        sequence.Enqueue(toPlay);
        sequence.Enqueue(current);
        playing = StartCoroutine(Play());
    }

    public void PlayAtTheEnd(string name){
        Animation2D toPlay = thisUnitSprites.GetAnimation(name);
        sequence.Enqueue(toPlay);
    }

    public void PlayThenStop(string name){
        Stop();
        sequence.Enqueue(thisUnitSprites.GetAnimation(name));
        playing = StartCoroutine(PlayOnce());
    }

    IEnumerator PlayOnce(){
        current = sequence.Dequeue();


        float timePerFrame = 1/current.frameRate; 
        for (int i = 0; i < current.frames.Count; i++){
            SR.sprite = current.frames[i];
            yield return new WaitForSeconds(timePerFrame);
        }
    }
}
