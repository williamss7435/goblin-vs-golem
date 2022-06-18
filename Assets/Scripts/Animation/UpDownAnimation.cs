using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpDownAnimation : MonoBehaviour
{
    Vector3 startPosition;
    bool on;
    void Start()
    {
        on = true;
        startPosition = GetComponent<RectTransform>().position;
        StartCoroutine(InfiniteUpDown());
    }

    private IEnumerator InfiniteUpDown(){
        int id;
        while(on){
            id = LeanTween.move(this.gameObject, new Vector3(startPosition.x, startPosition.y+3, startPosition.z), 1f).id;
            while(LeanTween.descr(id) != null) yield return null;

            id = LeanTween.move(this.gameObject, new Vector3(startPosition.x, startPosition.y-3, startPosition.z), 1f).id;
            while(LeanTween.descr(id) != null) yield return null;
        }
        
    }
}
