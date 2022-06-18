using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenuState : State
{
    public Image background;
    private void Start() {
        Enter();
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
        StartCoroutine(StartGame(1, 1f));
    }

    public void PressAnyButton(object sender, object args){
        Exit();
    }

    private IEnumerator StartGame(float endValue, float duration){
         
         float elapsedTime = 0;
         float startValue = background.color.a;

         while (elapsedTime < duration)
         {
             elapsedTime += Time.deltaTime;
             float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
             background.color = new Color(
                background.color.r, 
                background.color.g, 
                background.color.b, 
                newAlpha
            );
             yield return null;
        }

        SceneManager.LoadScene("Intro");
     }
}
