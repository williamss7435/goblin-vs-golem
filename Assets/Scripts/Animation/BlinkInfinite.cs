using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkInfinite : MonoBehaviour
{
    
    bool on;
    void Start()
    {
        on = true;
        StartCoroutine(InfiniteUpDown());
    }

    private IEnumerator InfiniteUpDown(){
        int id;
        RectTransform rectTransform = GetComponent<RectTransform>();
        while(on){
            id = LeanTween.alphaText(rectTransform, 0, 0.5f).id;
            while(LeanTween.descr(id) != null) yield return null;

            id = LeanTween.alphaText(rectTransform, 1, 0.5f).id;
            while(LeanTween.descr(id) != null) yield return null;
        }
        
    }
}
