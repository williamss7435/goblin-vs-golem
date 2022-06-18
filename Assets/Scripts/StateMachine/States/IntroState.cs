using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroState : State
{
    public Image background;
    public GameObject txtIntro;
    private void Start() {
        Enter();
        StartCoroutine(StartIntro());
    }
    public override void Enter()
    {
        base.Enter();
        InputController.instance.OnFire += PressAnyButton;
    }

    public override void Exit()
    {
        base.Exit();
        InputController.instance.OnFire -= PressAnyButton;
        StopAllCoroutines();
        SceneManager.LoadScene("Battle");
    }

    public void PressAnyButton(object sender, object args){
        Exit();
    }

    private IEnumerator StartIntro(){
        yield return StartCoroutine(BackScreenAlpha(0.6f));
        yield return null;
        yield return StartCoroutine(StartText());
        yield return null;
        yield return StartCoroutine(BackScreenAlpha(1f));
        yield return null;
        SceneManager.LoadScene("Battle");
    }
    private IEnumerator BackScreenAlpha(float alpha){
         
         float elapsedTime = 0;
         float startValue = background.color.a;
         float duration = 1f;

         while (elapsedTime < duration)
         {
             elapsedTime += Time.deltaTime;
             float newAlpha = Mathf.Lerp(startValue, alpha, elapsedTime / duration);
             background.color = new Color(
                background.color.r, 
                background.color.g, 
                background.color.b, 
                newAlpha
            );
             yield return null;
        }

        yield return null;
     }

     IEnumerator StartText(){
        Vector3 destinyPosition = new Vector3(txtIntro.transform.position.x, 1400, txtIntro.transform.position.z);
        var id = LeanTween.move(txtIntro, destinyPosition, 50f).id;
        while(LeanTween.descr(id) != null) yield return null;
        yield return null;
     }
}
