using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadPanel : MonoBehaviour
{
    public static LoadPanel instance; 
    private Text loadingText;

    private Text messageText;
    private Image loadPanelImage;
    private void Awake() {
        instance = this;
        loadingText = transform.Find("LoadingText").GetComponent<Text>();
        messageText = transform.Find("MessageText").GetComponent<Text>();
        loadPanelImage = transform.Find("BlackPanel").GetComponent<Image>();
    }
    
    public void Load(bool show){
        if(show)
            StartCoroutine(ShowLoad());
        else
            StartCoroutine(HideLoad());
    }

    public void GameLose(){
        StartCoroutine(GameOver());
    }

    public void GameWin(){
        StartCoroutine(EndScene());
    }

    private IEnumerator ShowLoad(){
        yield return StartCoroutine(SpriteFade(loadPanelImage, 1, 1f));
        yield return null;
        loadingText.gameObject.SetActive(true);
    }

     private IEnumerator HideLoad(){
        loadingText.gameObject.SetActive(false);
        yield return null;
        yield return StartCoroutine(SpriteFade(loadPanelImage, 0, 1f));
    }

    private IEnumerator ShowMessage(string message, Color color){
        messageText.text = message;
        messageText.color = color;
        yield return StartCoroutine(SpriteFade(loadPanelImage, 1, 1f));
        yield return StartCoroutine(SpriteFade(messageText, 1, 1f));
    }

    public IEnumerator ShowMessage(string message){
        messageText.text = message;
        messageText.color = new Color(255, 255, 255, 0);
        yield return StartCoroutine(SpriteFade(messageText, 1, 0.5f));
        yield return StartCoroutine(SpriteFade(messageText, 0, 0.5f));
    }

    private IEnumerator GameOver(){
        yield return StartCoroutine(ShowMessage("You Lose", new Color(255, 0, 0, 0)));
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartMenu");
    }

     private IEnumerator EndScene(){
        yield return StartCoroutine(ShowMessage("You Win", new Color(255, 255, 255, 0)));
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("EndScene");
    }

    private IEnumerator SpriteFade(MaskableGraphic sr, float endValue, float duration){
         
         float elapsedTime = 0;
         float startValue = sr.color.a;

         while (elapsedTime < duration)
         {
             elapsedTime += Time.deltaTime;
             float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
             sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
             yield return null;
         }

    }
}
